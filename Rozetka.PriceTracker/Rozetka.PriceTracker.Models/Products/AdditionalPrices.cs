using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rozetka.PriceTracker.Models.Products
{
    public class ProductAdditionalPrice
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [Required]
        public long ExternalId { get; set; }

        [Required]
        public decimal DiscountPrice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
