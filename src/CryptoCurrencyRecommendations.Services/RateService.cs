namespace CryptoCurrencyRecommendations.Services
{
    public class RateService : IRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RateService> _logger;
        private readonly IApplicationSettings _applicationSettings;
        public RateService(
            IHttpClientFactory httpClientFactory,
            ILogger<RateService> logger,
            IApplicationSettings applicationSettings)
        {
            (_httpClientFactory, _logger, _applicationSettings) = (httpClientFactory, logger, applicationSettings);
        }

        public async Task<T> GetFeeEstimate<T>(string coin)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync($"{_applicationSettings.Url}/{coin}/main");
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