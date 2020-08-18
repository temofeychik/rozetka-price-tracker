using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rozetka.PriceTracker.Models.Products;
using Rozetka.PriceTracker.Services.Options;
using Rozetka.PriceTracker.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Services.ProductLoader
{
    public class ProductLoaderService : IProductLoaderService
    {
        private readonly RozetkaOptions _rozetkaOptions;
        private readonly ILogger<ProductLoaderService> _logger;
        
        private readonly Regex ParseProductIdRegex = new Regex("https:\\/\\/(.+)?rozetka.com.ua\\/(?<name>\\w+)\\/p(?<id>\\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        public ProductLoaderService(IOptions<RozetkaOptions> rozetkaOptions, ILogger<ProductLoaderService> logger)
        {
            _rozetkaOptions = rozetkaOptions.Value;
            _logger = logger;
        }

        public async Task<Product> LoadProductAsync(string productHref)
        {
            try
            {
                long productId = ParseProductIdFromHref(productHref);

                if (productId > 0)
                {
                    var product = (await LoadRozetkaProductAsync(productId)).First(); ;

                    return new Product
                    {
                        CreatedOn = DateTime.Now,
                        Description = product.Description.Text,
                        Discount = product.Discount,
                        Href = product.Href,
                        ExternalProductId = product.Id,
                        ImageUrl = product.Images.FirstOrDefault()?.Original.Url,
                        Name = product.Name,
                        OldPrice = product.OldPrice,
                        Price = product.Price,
                        SellStatus = product.SellStatus,
                        Status = product.Status,
                        Title = product.Title
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred when trying to load information for product. href = {productHref}. \r\n {ex}");
            }

            return null;
        }

        public async Task<IEnumerable<Product>> LoadProducstAsync(params long[] productIds)
        {
            List<Product> products = new List<Product>();
            try
            {
                if (productIds.Any())
                {
                    var product = (await LoadRozetkaProductAsync(productIds.ToArray())).First();

                    products.Add(new Product
                    {
                        CreatedOn = DateTime.Now,
                        Description = product.Description.Text,
                        Discount = product.Discount,
                        Href = product.Href,
                        ExternalProductId = product.Id,
                        ImageUrl = product.Images.FirstOrDefault()?.Original.Url,
                        Name = product.Name,
                        OldPrice = product.OldPrice,
                        Price = product.Price,
                        SellStatus = product.SellStatus,
                        Status = product.Status,
                        Title = product.Title
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred when trying to load information for products. {ex}");
            }

            return products;
        }

        public async Task<IEnumerable<ProductAdditionalPrice>> LoadAdditionalPricesAsync(params long[] productIds)
        {
            List<ProductAdditionalPrice> additionalPrices = new List<ProductAdditionalPrice>();
            try
            {
                foreach (var productId in productIds)
                {

                    var productAdditionalPrices = (await LoadRozetkaAdditionalPricesAsync(productId))
                        .Select(x => new ProductAdditionalPrice
                        {
                            Description = x.Description,
                            DiscountPrice = x.DiscountPrice,
                            ExternalId = x.Id,
                            IsActive = true,
                            ProductId = productId,
                            Title = x.Title,
                            LastUpdated = DateTime.Now
                        });

                    additionalPrices.AddRange(productAdditionalPrices);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occured when trying to load product additional prices. {ex}");
            }

            return additionalPrices;
        }

        private async Task<IEnumerable<RozetkaProductAdditionalPrice>> LoadRozetkaAdditionalPricesAsync(long productId)
        {
            HttpClient httpClient = null;
            List<RozetkaProductAdditionalPrice> loadedProducts = new List<RozetkaProductAdditionalPrice>();
            try
            {
                using (httpClient = new HttpClient())
                {

                    HttpRequestMessage getProductAdditionalPricesRequset = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(string.Format(_rozetkaOptions.ProductAdditionalPRicecUrlTemplate, productId)),
                        Method = HttpMethod.Get
                    };

                    var getProductResponse = await httpClient.SendAsync(getProductAdditionalPricesRequset);

                    if (getProductResponse.IsSuccessStatusCode)
                    {
                        var responseContent = await getProductResponse.Content.ReadAsStringAsync();
                        loadedProducts.AddRange(JsonConvert.DeserializeObject<RozetkaProductAdditionalPricesViewModel>(responseContent)?.Data?.Prices);
                    }

                }
            }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }

            return loadedProducts;
        }

        private async Task<IEnumerable<RozetkaProduct>> LoadRozetkaProductAsync(params long[] productIds)
        {
            HttpClient httpClient = null;

            List<RozetkaProduct> loadedProducts = new List<RozetkaProduct>();
            try
            {
                using (httpClient = new HttpClient())
                {
                    foreach (var id in productIds)
                    {
                        HttpRequestMessage getProductRequset = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(string.Format(_rozetkaOptions.ProductApiUrlTemplate, id)),
                            Method = HttpMethod.Get
                        };

                        var getProductResponse = await httpClient.SendAsync(getProductRequset);

                        if (getProductResponse.IsSuccessStatusCode)
                        {
                            var responseContent = await getProductResponse.Content.ReadAsStringAsync();
                            loadedProducts.Add(JsonConvert.DeserializeObject<RozetkaProductResponseViewModel>(responseContent)?.Data);
                        }
                    }
                }
            }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }

            return loadedProducts;
        }

        private long ParseProductIdFromHref(string href)
        {
            
            long productId = -1;
            try
            {
                Match match = ParseProductIdRegex.Match(href);

                if (match.Success)
                {
                    string productIdValue = match.Groups["id"].Value;

                    if (string.IsNullOrEmpty(productIdValue))
                        return -1;

                    if (long.TryParse(productIdValue, out productId))
                    {
                        return productId;
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error has occurred when trying to get product id from href. Href = {href}. {ex}");
            }


            return productId;
        }

    }
}
