namespace CryptoCurrencyRecommendations.Api.Configurations
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Main { get; set; } = string.Empty;
    }
}