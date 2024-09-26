using Demo_APP.Helpers;
using Demo_APP.Interfaces;
using Demo_APP.Models;
using Demo_APP.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables()
       .Build();
builder.Services.Configure<HighMobilityConfig>(configuration.GetSection("HighMobility"));

// Register HighMobilityConfig for dependency injection
builder.Services.AddSingleton<HttpClient>();

// Register your HighMobilityApiCaller class as a singleton (or scoped/transient as per your need)
builder.Services.AddSingleton<IHighMobilityApiCaller, HighMobilityApiCaller>();

// Register the HighMobilityConfig if needed
builder.Services.AddScoped<ICallHighMobilityService, CallHighMobilityService>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
builder.Services.AddScoped<IUpdateAppSettingsService, UpdateAppSettingsService>();
builder.Services.AddSingleton<IHighMobilityAuthService, HighMobilityAuthService>();
builder.Services.AddScoped<IFirebaseConfigHelperService, FirebaseConfigHelperService>();
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HighMobilityConfig>>().Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
