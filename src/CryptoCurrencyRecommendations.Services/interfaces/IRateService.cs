using CryptoCurrencyRecommendations.Services.Dtos;

namespace CryptoCurrencyRecommendations.Domain.interfaces
{
    public interface IRateService
    {
        Task<T> GetFeeEstimate<T>(string coin);
    }
}