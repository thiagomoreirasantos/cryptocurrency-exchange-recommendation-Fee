using CryptoCurrencyRecommendations.Services;

namespace CryptoCurrencyRecommendations.Api.Extensions
{
    public static class Mapper
    {
        public static Dtos.FeeEstimateOutput MapToOutput(this FeeEstimate feeEstimate) =>
            new()
            {
                HighFeePerKb = feeEstimate.HighFeePerKb,
                MediumFeePerKb = feeEstimate.MediumFeePerKb,
                LowFeePerKb = feeEstimate.LowFeePerKb
            };
    }
}