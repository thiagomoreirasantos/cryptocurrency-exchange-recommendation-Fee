using CryptoCurrencyRecommendations.Api.Configurations;
using CryptoCurrencyRecommendations.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var appsettings = builder.Configuration.GetSection("AppSettings").Get<ApplicationSettings>() ?? throw new InvalidOperationException("Unable to get appsettings");

builder.Services.AddSingleton<IApplicationSettings>(appsettings);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("blockcypher", c =>
{
    c.BaseAddress = new Uri("https://api.blockcypher.com/v1/");
});

builder.Services.AddScoped<IRateService, RateService>();

var app = builder.Build();

app.MapGet("/v1/fee-estimate/kb/btc", async (IRateService rateService) =>
{
    string coin = GetCoin(Coin.btc);

    var feeEstimate = await rateService.GetFeeEstimate<BTCFeeEstimate>(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate.MapBTCToOutput());
}).AddEndpointFilter<ApiKeyAuthenticationFilter>();

app.MapGet("/v1/fee-estimate/kb/eth", async (IRateService rateService) =>
{
    var coin = GetCoin(Coin.eth);

    var feeEstimate = await rateService.GetFeeEstimate<ETHFeeEstimate>(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate.MapETHToOutput());
}).AddEndpointFilter<ApiKeyAuthenticationFilter>();;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static string GetCoin(Coin coin)
{
    return Enum.GetName(typeof(Coin), coin) ?? throw new InvalidOperationException("Unable to get coin");
}