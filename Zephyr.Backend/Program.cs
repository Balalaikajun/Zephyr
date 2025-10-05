using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Dadata;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Conventions;
using Zephyr.Backend.Infrastructure.Extensions;
using Zephyr.Backend.Infrastructure.Mapping;
using Zephyr.Backend.Infrastructure.Middlewares;
using Zephyr.Backend.Services;
using Zephyr.Backend.Services.Interfaces;
using Zephyr.Backend.Utils.Weather.ApiClients;
using Zephyr.Backend.Utils.Weather.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.Configure<Secrets>(builder.Configuration.GetSection("Secrets"));

builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Settings>>().Value);
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Secrets>>().Value);

builder.Services.AddHttpClient<OpenWeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<ISuggestClientAsync, SuggestClientAsync>(x =>
    new SuggestClientAsync(x.GetRequiredService<Secrets>().DadataToken));

builder.Services.AddWeatherApiClients(typeof(IWeatherApiClient).Assembly);

builder.Services.AddAutoMapper(x => { }, typeof(CoreMapping));

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

    options.OperationFilter<LowercaseQueryParameterOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
});

app.UseMiddleware<HandleExceptionMiddleware>();

app.UseAuthorization();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.MapControllers();

app.Run();