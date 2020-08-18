using Microsoft.EntityFrameworkCore;
using Rozetka.PriceTracker.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rozetka.PriceTracker.EntityFramework.DbContext
{
    public class RozetkaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAdditionalPrice> ProductAdditionalPrices { get; set; }

        public RozetkaDbContext(DbContextOptions<RozetkaDbContext> options)
            : base(options)
        {

        }

        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var priceHistory = builder.Entity<ProductPriceHistoty>();

            priceHistory.HasOne(x => x.Product)
                .WithMany(x => x.PriceHistory)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
