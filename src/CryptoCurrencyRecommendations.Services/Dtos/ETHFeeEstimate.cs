using Newtonsoft.Json;

namespace CryptoCurrencyRecommendations.Services.Dtos
{
    public class ETHFeeEstimate : FeeEstimate
    {
        [JsonProperty("high_gas_price")]
        public long HighGasPrice { get; set; }

        [JsonProperty("medium_gas_price")]
        public long MediumGasPrice { get; set; }

        [JsonProperty("low_gas_price")]
        public long LowGasPrice { get; set; }

        [JsonProperty("high_priority_fee")]
        public long HighPriorityFee { get; set; }

        [JsonProperty("medium_priority_fee")]
        public long MediumPriorityFee { get; set; }

        [JsonProperty("low_priority_fee")]
        public long LowPriorityFee { get; set; }
    }
}