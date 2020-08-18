using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Rozetka.PriceTracker.Services.ProductLoader;
using Rozetka.PriceTracker.Services.Products;

namespace Rozetka.PriceTracker.Grpc.Services
{
    public class PriceTrackerService : PriceTracker.PriceTrackerBase
    {
        private readonly ILogger<PriceTrackerService> _logger;
        private readonly IProductLoaderService _productLoaderService;
        private readonly IProductsService _productsService;

        public PriceTrackerService(ILogger<PriceTrackerService> logger,
            IProductLoaderService productLoaderService,
            IProductsService productsService)
        {
            _logger = logger;
            _productLoaderService = productLoaderService;
            _productsService = productsService;
        }

        public override async Task<TrackProductResponse> TrackProduct(TrackProductRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _productLoaderService.LoadProductAsync(request.ProductUrl);

                if (product != null)
                {
                    var savedProduct = await _productsService.AddOrUpdateProductAsync(product);

                    if (savedProduct != null)
                    {
                        var productResponse = new TrackProductResponse
                        {
                            Description = savedProduct.Description,
                            Discount = savedProduct.Discount,
                            Id = (int)savedProduct.Id,
                            ImageUrl = savedProduct.ImageUrl,
                            Price = (float)savedProduct.Price,
                            Title = savedProduct.Title,
                            Url = savedProduct.Href,
                            SellStatus = savedProduct.SellStatus,
                            Status = savedProduct.Status,
                            PrevPrice = (float)(savedProduct.PriceHistory?.OrderByDescending(x => x.LastUpdated).FirstOrDefault()?.Price ?? savedProduct.Price)
                        };

                        productResponse.AdditionalPrices.AddRange(savedProduct.AdditionalPrices.Select(x => new ProductAdditionalPricesResponse
                        {
                            Description = x.Description,
                            DiscountPrice = (float)x.DiscountPrice,
                            Id = x.Id,
                            LastUpdatedOn = Timestamp.FromDateTime(x.LastUpdated),
                            ProductId = x.ProductId,
                            Title = x.Title
                        }));

                        return productResponse;
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error has occurred when trying to add product for price tracking. {ex}");
            }

            return null;

        }

        public override async Task TrackProductStream(TrackProductRequest request, IServerStreamWriter<TrackProductResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var product = await _productLoaderService.LoadProductAsync(request.ProductUrl);

                if (product != null)
                {
                    await responseStream.WriteAsync(
                            new TrackProductResponse
                            {
                                Description = product.Description,
                                Discount = product.Discount,
                                Id = (int)product.Id,
                                ImageUrl = product.ImageUrl,
                                Price = (float)product.Price,
                                Title = product.Title,
                                Url = product.Href,
                                SellStatus = product.SellStatus,
                                Status = product.Status,
                            });


                }

                await Task.Delay(TimeSpan.FromSeconds(15));
            }

            if (context.CancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Stream canceled");
            }
        }

        public override async Task<TrackProductPriceResponse> TrackPrices(Empty request, ServerCallContext context)
        {
            try
            {
                var productsList = await _productsService.GetProductsAsync();

                var response = new TrackProductPriceResponse();
                foreach (var product in productsList)
                {
                    var responseProduct = new TrackProductResponse
                    {
                        Description = product.Description,
                        Discount = product.Discount,
                        Id = (int)product.Id,
                        ImageUrl = product.ImageUrl,
                        Price = (float)product.Price,
                        Title = product.Title,
                        Url = product.Href,
                        SellStatus = product.SellStatus,
                        Status = product.Status,
                        PrevPrice = (float)(product.PriceHistory?.OrderByDescending(x => x.LastUpdated).FirstOrDefault()?.Price ?? product.Price)
                    };

                    responseProduct.AdditionalPrices.AddRange(product.AdditionalPrices.Select(x => new ProductAdditionalPricesResponse
                    {
                        Description = x.Description,
                        DiscountPrice = (float)x.DiscountPrice,
                        Id = x.Id,
                        LastUpdatedOn = Timestamp.FromDateTime(x.LastUpdated.ToUniversalTime()),
                        ProductId = x.ProductId,
                        Title = x.Title
                    }));

                    response.Products.Add(responseProduct);
                }

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error has occurred when trying to get products to prices track. {ex}");
            }

            return null;

        }

        public override async Task<ProductInfoResponse> GetProductInfo(GetProductInfoRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _productsService.GetProductAsync(request.ProductId);


                if (product == null)
                    return null;

                var response = new ProductInfoResponse();

                response.Prices.AddRange(product.PriceHistory.Select(x => new PorductPriceHistoryResponse
                {
                    ProductId = product.Id,
                    Id = x.Id,
                    Date = Timestamp.FromDateTime(x.LastUpdated.ToUniversalTime()),
                    Price = (float)x.Price
                }));

                response.ProductInfo = new TrackProductResponse
                {
                    Description = product.Description,
                    Discount = product.Discount,
                    Id = (int)product.Id,
                    ImageUrl = product.ImageUrl,
                    Price = (float)product.Price,
                    Title = product.Title,
                    Url = product.Href,
                    SellStatus = product.SellStatus,
                    Status = product.Status
                };

                response.ProductInfo.AdditionalPrices.AddRange(product.AdditionalPrices.Select(x => new ProductAdditionalPricesResponse
                {
                    Description = x.Description,
                    DiscountPrice = (float)x.DiscountPrice,
                    Id = x.Id,
                    LastUpdatedOn = Timestamp.FromDateTime(x.LastUpdated.ToUniversalTime()),
                    ProductId = x.ProductId,
                    Title = x.Title
                }));

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred when trying to get product info. {ex}");
            }

            return null;
        }

    }
}
