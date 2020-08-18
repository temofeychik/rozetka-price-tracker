using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rozetka.PriceTracker.Models.Products
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long ExternalProductId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal OldPrice { get; set; }

        [Required]
        public string Href { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SellStatus { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public float Discount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime LastUpdateOn { get; set; }

        public virtual IEnumerable<ProductPriceHistoty> PriceHistory { get; set; }

        public virtual IEnumerable<ProductAdditionalPrice> AdditionalPrices { get; set; }
    }
}

