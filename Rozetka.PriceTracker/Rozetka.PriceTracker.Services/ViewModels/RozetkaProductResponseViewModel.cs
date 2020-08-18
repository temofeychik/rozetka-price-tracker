using Newtonsoft.Json;
using System.Collections.Generic;

namespace Rozetka.PriceTracker.Services.ViewModels
{
    public class RozetkaProductResponseViewModel
    {
        public RozetkaProduct Data { get; set; }

    }

    public class RozetkaProduct
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("old_price")]
        public decimal OldPrice { get; set; }

        public string Href { get; set; }

        public string Name { get; set; }

        [JsonProperty("sell_status")]
        public string SellStatus { get; set; }

        public string Status { get; set; }

        public float Discount { get; set; }

        public ProductDescription Description { get; set; }

        public IEnumerable<RozetkaProductImageContainer> Images { get; set; }
    }

    public class ProductDescription
    {
        public string Text { get; set; }
    }
}
