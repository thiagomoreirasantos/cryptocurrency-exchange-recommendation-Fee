using Newtonsoft.Json;

namespace CryptoCurrencyRecommendations.Services
{
    public class FeeEstimate
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("hash")]
        public string? Hash { get; set; }

        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("latest_url")]
        public string? LatestUrl { get; set; }

        [JsonProperty("previous_hash")]
        public string? PreviousHash { get; set; }

        [JsonProperty("previous_url")]
        public string? PreviousUrl { get; set; }

        [JsonProperty("peer_count")]
        public int PeerCount { get; set; }

        [JsonProperty("unconfirmed_count")]
        public int UnconfirmedCount { get; set; }

        [JsonProperty("high_fee_per_kb")]
        public long HighFeePerKb { get; set; }

        [JsonProperty("medium_fee_per_kb")]
        public long MediumFeePerKb { get; set; }

        [JsonProperty("low_fee_per_kb")]
        public long LowFeePerKb { get; set; }

        [JsonProperty("last_fork_height")]
        public long LastForkHeight { get; set; }

        [JsonProperty("last_fork_hash")]
        public string? LastForkHash { get; set; }        
    }
}