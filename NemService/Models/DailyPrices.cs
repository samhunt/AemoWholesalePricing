using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nem
{
    public class DailyPrices
    {
        [JsonPropertyName("5MIN")]
        public List<NEM_FORECAST_PRICE> Prices { get; set; }
    }

}