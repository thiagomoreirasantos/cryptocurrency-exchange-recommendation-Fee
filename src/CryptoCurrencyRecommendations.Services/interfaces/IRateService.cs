using CryptoCurrencyRecommendations.Services;

namespace CryptoCurrencyRecommendations.Domain.interfaces
{
    public interface IRateService
    {
        Task<FeeEstimate> GetFeeEstimateByCoin(string coin);
    }
}