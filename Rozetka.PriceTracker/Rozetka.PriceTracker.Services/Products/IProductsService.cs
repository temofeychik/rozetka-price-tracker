using Rozetka.PriceTracker.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Services.Products
{
    public interface IProductsService
    {
        Task<Product> AddOrUpdateProductAsync(Product product);
        Task<Product> GetProductAsync(long productId);
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}