namespace CryptoCurrencyRecommendations.Services.Dtos
{
    public class FeeEstimateOutput
    {
        public long HighFeePerKb { get; set; }
        public long MediumFeePerKb { get; set; }
        public long LowFeePerKb { get; set; }
    }
}