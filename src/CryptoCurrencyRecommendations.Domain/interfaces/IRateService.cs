namespace CryptoCurrencyRecommendations.Domain.interfaces
{
    public interface IRateService
    {
        Task<FeeEstimate> GetFeeEstimate(string coin);
    }
}