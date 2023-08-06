using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var appsettings = builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>() ?? throw new InvalidOperationException("Unable to get appsettings");
builder.Services.AddSingleton<IApplicationSettings>(appsettings);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CryptoCurrencyRecommendations.Api", Version = "v1" });

    c.AddSecurityDefinition("apikey", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "apiKey",
        Description = "Authorization query string expects API key"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "apikey"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpClient();
builder.Services.AddHttpClient(appsettings.Main, c =>
{
    c.BaseAddress = new Uri(appsettings.Url);
});

builder.Services.AddScoped<IRateService, RateService>();

var app = builder.Build();

app.MapGet("/v1/fee-estimate/{coin}/per-kb", async ([FromRoute(Name = "coin")] string coin, IRateService rateService) =>
{
    var feeEstimate = await rateService.GetFeeEstimateByCoin(coin);
    if (feeEstimate is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(feeEstimate.GetOutput());
}).AddEndpointFilter<ApiKeyAuthenticationFilter>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();