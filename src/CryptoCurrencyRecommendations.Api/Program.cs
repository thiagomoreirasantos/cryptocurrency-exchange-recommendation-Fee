using CryptoCurrencyRecommendations.Domain.interfaces;
using CryptoCurrencyRecommendations.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("blockcypher", c =>
{
    c.BaseAddress = new Uri("https://api.blockcypher.com/v1/");
});

builder.Services.AddScoped<IRateService, RateService>();

var app = builder.Build();

app.MapGet("/fee-estimate/{coin}", ([FromRoute(Name = "coin")] string coin, IRateService rateService) =>
{
    var feeEstimate = rateService.GetFeeEstimate(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate);
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
