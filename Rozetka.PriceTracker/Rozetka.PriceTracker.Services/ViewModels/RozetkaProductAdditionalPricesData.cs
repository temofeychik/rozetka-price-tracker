using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Rozetka.PriceTracker.Services.ViewModels
{
    public class RozetkaProductAdditionalPricesData
    {
        public IEnumerable<RozetkaProductAdditionalPrice> Prices { get; set; }

        public JObject Promotion { get; set; } // ?? unknown data here...
    }
}
