using CryptoCurrencyRecommendations.Domain.interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CryptoCurrencyRecommendations.Services
{
    public class RateService : IRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RateService> _logger;
        public RateService(
            IHttpClientFactory httpClientFactory,
            ILogger<RateService> logger) =>
            (_httpClientFactory, _logger) = (httpClientFactory, logger);

        public async Task<T> GetFeeEstimate<T>(string coin)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync($"https://api.blockcypher.com/v1/{coin}/main");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                var feeEstimate = JsonConvert.DeserializeObject<T>(result) ?? throw new InvalidOperationException("Unable to deserialize fee estimate");
                return feeEstimate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting fee estimate");
                throw;
            }
        }
    }
}