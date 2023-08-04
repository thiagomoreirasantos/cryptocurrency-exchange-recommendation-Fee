using CryptoCurrencyRecommendations.Api.Configurations;

namespace CryptoCurrencyRecommendations.Api.Filters
{
    public class ApiKeyAuthenticationFilter : IEndpointFilter
    {
        private readonly IApplicationSettings _applicationSettings;

        public ApiKeyAuthenticationFilter(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var apiKey = context.HttpContext.Request.Headers["ApiKey"].ToString();

            if (!string.IsNullOrEmpty(apiKey) || string.IsNullOrWhiteSpace(apiKey) || !string.Equals(apiKey, _applicationSettings.ApiKey))
            {
                return Results.Unauthorized();
            }
            return await next(context);
        }
    }
}
