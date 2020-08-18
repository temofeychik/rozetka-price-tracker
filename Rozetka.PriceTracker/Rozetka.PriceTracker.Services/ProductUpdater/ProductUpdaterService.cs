using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using Rozetka.PriceTracker.Models.Products;
using Rozetka.PriceTracker.Services.ProductLoader;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Services.ProductUpdater
{
    public class ProductUpdaterService : IProductUpdaterService
    {
        private readonly IProductLoaderService _productLoader;
        private readonly RozetkaDbContext _context;
        private readonly ILogger<ProductUpdaterService> _logger;
        public ProductUpdaterService(RozetkaDbContext context, 
            IProductLoaderService productLoader,
            ILogger<ProductUpdaterService> logger)
        {
            _productLoader = productLoader;
            _context = context;
            _logger = logger;
        }

        public async Task UpdatePrices(TimeSpan timeBetweenUpdates, int amount = 1000)
        {
            try
            {
                var nextDate = DateTime.Now.Add(timeBetweenUpdates * -1);

                var productsToUpdate = await _context.Products
                    .Include(x => x.PriceHistory)
                    .Include(x=>x.AdditionalPrices)
                    .Where(X => X.LastUpdateOn < nextDate)
                    .Take(amount)
                    .ToListAsync();

                var productIdsArray = productsToUpdate.Select(x => x.ExternalProductId).ToArray();

                var loadedProducts = await _productLoader.LoadProducstAsync(productIdsArray);

                foreach (var loadedProduct in loadedProducts)
                {
                    var savedProduct = productsToUpdate.FirstOrDefault(x => x.ExternalProductId == loadedProduct.ExternalProductId);

                    if (savedProduct != null)
                    {
                        if (savedProduct.Price != loadedProduct.Price)
                        {
                            _context.Add(new ProductPriceHistoty
                            {
                                LastUpdated = DateTime.Now,
                                Price = savedProduct.Price,
                                ProductId = savedProduct.Id
                            });

                        }


                        savedProduct.Price = loadedProduct.Price;
                        savedProduct.OldPrice = loadedProduct.OldPrice;
                        savedProduct.SellStatus = loadedProduct.SellStatus;
                        savedProduct.Status = loadedProduct.Status;
                        savedProduct.Title = loadedProduct.Title;
                        savedProduct.Name = loadedProduct.Name;
                        savedProduct.ImageUrl = loadedProduct.ImageUrl;
                        savedProduct.Href = loadedProduct.Href;
                        savedProduct.LastUpdateOn = DateTime.Now;
                        savedProduct.Description = loadedProduct.Description;

                        _context.Update(savedProduct);
  
                    }
                }

                await _context.SaveChangesAsync();

                foreach (var product in productsToUpdate)
                {
                    var loadedAdditionalPrices = await _productLoader.LoadAdditionalPricesAsync(product.ExternalProductId);

                    if(product.AdditionalPrices.Count() != loadedAdditionalPrices.Count())
                    {
                        foreach(var productAdditionalPrice in product.AdditionalPrices)
                        {
                            productAdditionalPrice.IsActive = false;
                        }

                        _context.UpdateRange(product.AdditionalPrices);
                    }

                    foreach (var additionalPrice in loadedAdditionalPrices)
                    {
                        additionalPrice.ProductId = product.Id;

                        var savedAdditionalPrice = product?.AdditionalPrices?.FirstOrDefault(x => x.ExternalId == additionalPrice.ExternalId && x.IsActive);
                       
                        if (savedAdditionalPrice == null)
                        {
                            _context.ProductAdditionalPrices.Add(additionalPrice);
                            continue;
                        }

                        savedAdditionalPrice.Description = additionalPrice.Description;
                        savedAdditionalPrice.DiscountPrice = additionalPrice.DiscountPrice;
                        savedAdditionalPrice.Title = additionalPrice.Title;

                        _context.ProductAdditionalPrices.Update(savedAdditionalPrice);
                    }
                }

               

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred when tryign to update product prices {ex}");
            }
        }

    }
}
