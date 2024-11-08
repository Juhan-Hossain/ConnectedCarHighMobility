using Demo_APP.Helpers;
using Demo_APP.Interfaces;
using Demo_APP.Models;
using Demo_APP.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HighMobilityConfig>>().Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // You can change this to an empty string to serve at the root URL
});

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();
