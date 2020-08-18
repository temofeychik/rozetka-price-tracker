using Newtonsoft.Json;

namespace Rozetka.PriceTracker.Services.ViewModels
{
    public class RozetkaProductAdditionalPrice
    {
        public long Id { get; set; }

        public string Description { get; set; }

        [JsonProperty("discount_price")]
        public decimal DiscountPrice { get; set; }

        public int Order { get; set; }

        [JsonProperty("promo_code")]
        public string PromoCode { get; set; }

        [JsonProperty("show_description_and_price_in_details")]
        public bool ShowDescriptionAndPriceInDetails { get; set; }

        [JsonProperty("show_in_details")]
        public bool ShowInDetails { get; set; }

        public string Title { get; set; }
    }
}
