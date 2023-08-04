namespace CryptoCurrencyRecommendations.Api.Configurations
{
    public class ApplicationSettings : IApplicationSettings
    {
        public string ApiKey { get; } = string.Empty;
    }
}