using Demo_APP.Helpers;
using Demo_APP.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

public class HighMobilityApiCaller : IHighMobilityApiCaller
{
    private readonly HttpClient _client;
    private readonly IHighMobilityAuthService _highMobilityAuthService;
    private HighMobilityConfig _config;

    public HighMobilityApiCaller(HttpClient client, IOptions<HighMobilityConfig> config, IHighMobilityAuthService highMobilityAuthService)
    {
        _client = client;
        _highMobilityAuthService = highMobilityAuthService;
        _config = config.Value;

        // Configure the HttpClient once in the constructor
        if (_client.BaseAddress == null) // Set BaseAddress if it's not already set
        {
            _client.BaseAddress = new Uri(_config.ApiBaseUrl);
        }

        if (!_client.DefaultRequestHeaders.Contains("Authorization") || _config.AccessToken != _highMobilityAuthService.GetAccessTokenAsync().Result)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _highMobilityAuthService.GetAccessTokenAsync().Result);
        }
    }

    public async Task<object?> CallApiAsync(string endpoint)
    {
        var response = await _client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<object>();
    }
}
