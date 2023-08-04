using Newtonsoft.Json;

namespace CryptoCurrencyRecommendations.Services.Dtos
{
    public class BTCFeeEstimate: FeeEstimate
    {
        [JsonProperty("high_fee_per_kb")]
        public long HighFeePerKb { get; set; }

        [JsonProperty("medium_fee_per_kb")]
        public long MediumFeePerKb { get; set; }

        [JsonProperty("low_fee_per_kb")]
        public long LowFeePerKb { get; set; }
    }
}