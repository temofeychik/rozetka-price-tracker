using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rozetka.PriceTracker.Models.Products
{
    public class ProductPriceHistoty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public decimal Price { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

