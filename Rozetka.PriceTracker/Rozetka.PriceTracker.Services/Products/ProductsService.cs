using Microsoft.EntityFrameworkCore;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using Rozetka.PriceTracker.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly RozetkaDbContext _context;
        public ProductsService(RozetkaDbContext rozetkaDbContext)
        {
            _context = rozetkaDbContext;
        }

        public async Task<Product> AddOrUpdateProductAsync(Product product)
        {
            var savedProduct = await _context.Products.FirstOrDefaultAsync(x => x.ExternalProductId == product.ExternalProductId);

            if (savedProduct != null)
            {
                savedProduct.Href = product.Href;
                savedProduct.ImageUrl = product.ImageUrl;
                savedProduct.CreatedOn = product.CreatedOn;
                savedProduct.LastUpdateOn = DateTime.Now;
                savedProduct.Description = product.Description;
                savedProduct.Discount = product.Discount;
                savedProduct.ExternalProductId = product.ExternalProductId;
                savedProduct.Name = product.Name;
                savedProduct.OldPrice = product.OldPrice;
                savedProduct.Price = product.Price;
                savedProduct.SellStatus = product.SellStatus;
                savedProduct.Status = product.Status;
                savedProduct.Title = product.Title;

                _context.Update(savedProduct);

            }
            else
            {
                savedProduct = product;
                _context.Add(savedProduct);
            }

            if (await _context.SaveChangesAsync() > 0)
            {
                return savedProduct;
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.PriceHistory)
                .Include(x => x.AdditionalPrices)
                .OrderBy(x => x.SellStatus)
                .ThenByDescending(x => x.CreatedOn)
                .ToListAsync();
        }

        public async Task<Product> GetProductAsync(long productId)
        {
            return await _context.Products
                 .AsNoTracking()
                 .Include(x => x.PriceHistory)
                 .Include(x => x.AdditionalPrices)
                 .FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
