using CryptoCurrencyRecommendations.Api.Extensions;
using CryptoCurrencyRecommendations.Domain.interfaces;
using CryptoCurrencyRecommendations.Services;

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

app.MapGet("/v1/fee-estimate/{coin}", async (string coin, IRateService rateService) =>
{
    var feeEstimate = await rateService.GetFeeEstimate(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate.MapToOutput());
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
