using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Dadata;
using Microsoft.OpenApi.Models;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Conventions;
using Zephyr.Backend.Infrastructure.Middlewares;
using Zephyr.Backend.Services;
using Zephyr.Backend.Services.Interfaces;
using Zephyr.Backend.Utils;
using Zephyr.Backend.Utils.ApiClients;
using Zephyr.Backend.Utils.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.Configure<Secrets>(builder.Configuration.GetSection("Secrets"));

builder.Services.AddHttpClient<OpenWeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherClientFactory, WeatherClientFactory>();
builder.Services.AddScoped<OpenWeatherClient>();
builder.Services.AddScoped<OpenMeteoClient>();
builder.Services.AddScoped<YandexWeatherClient>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<ISuggestClientAsync, SuggestClientAsync>(x =>
    new SuggestClientAsync(builder.Configuration.GetSection("Secrets")["DadataToken"]));

builder.Services.AddAutoMapper(x => { }, typeof(MappingProfile));

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddControllers(options => { options.Conventions.Add(new RoutePrefixConvention("api")); })
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(0, 1);
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v0.1", new OpenApiInfo
    {
        Title = "Zephyr API",
        Version = "v0.1",
        Description = "API v0.1 для получения данных о погоде"
    });
});

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

app.UseMiddleware<HandleExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();