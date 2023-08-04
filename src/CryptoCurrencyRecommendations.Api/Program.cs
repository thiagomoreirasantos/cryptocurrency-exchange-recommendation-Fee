using CryptoCurrencyRecommendations.Api.Extensions;
using CryptoCurrencyRecommendations.Api.Helper;
using CryptoCurrencyRecommendations.Domain.interfaces;
using CryptoCurrencyRecommendations.Services;
using CryptoCurrencyRecommendations.Services.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
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
    var coin = Enum.GetName(typeof(Coin), Coin.btc)?? throw new InvalidOperationException("Unable to get coin");

    var feeEstimate = await rateService.GetFeeEstimate<BTCFeeEstimate>(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate.MapBTCToOutput());
});

app.MapGet("/v1/fee-estimate/kb/eth", async (IRateService rateService) =>
{
    var coin = Enum.GetName(typeof(Coin), Coin.eth)?? throw new InvalidOperationException("Unable to get coin");

    var feeEstimate = await rateService.GetFeeEstimate<ETHFeeEstimate>(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate.MapETHToOutput());
});

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
