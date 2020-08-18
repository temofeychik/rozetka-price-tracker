using Rozetka.PriceTracker.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Services.ProductLoader
{
    public interface IProductLoaderService
    {
        Task<IEnumerable<ProductAdditionalPrice>> LoadAdditionalPricesAsync(params long[] productIds);
        Task<IEnumerable<Product>> LoadProducstAsync(params long[] productIds);
        Task<Product> LoadProductAsync(string productHref);
    }
}