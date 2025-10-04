using Dadata;
using Microsoft.Extensions.Options;
using Zephyr.Backend.Contracts.Interfaces;
using Zephyr.Backend.Infrastructure;
using Zephyr.Backend.Infrastructure.Conventions;
using Zephyr.Backend.Services;
using Zephyr.Backend.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.Configure<Secrets>(builder.Configuration.GetSection("Secrets"));

var serviceProvider = builder.Services.BuildServiceProvider();
var settings = serviceProvider.GetRequiredService<IOptions<Settings>>().Value;
var secrets = serviceProvider.GetRequiredService<IOptions<Secrets>>().Value;

builder.Services.AddHttpClient<OpenWeatherClient>();
builder.Services.AddScoped<IWeatherApiClient, OpenWeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();

builder.Services.AddScoped<ISuggestClientAsync, SuggestClientAsync>(x => new SuggestClientAsync(secrets.DadataToken));

builder.Services.AddAutoMapper(x => { }, typeof(MappingProfile));

builder.Services.AddControllers(options => { options.Conventions.Add(new RoutePrefixConvention("api")); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();