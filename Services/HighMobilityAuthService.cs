using Demo_APP.Dtos;
using Demo_APP.Interfaces;
using Google.Apis.Auth.OAuth2.Responses;
using Newtonsoft.Json;
using System.Text;

namespace Demo_APP.Services
{
    public class HighMobilityAuthService : IHighMobilityAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _accessToken;

        public HighMobilityAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            // Return cached token if it exists
            if (!string.IsNullOrEmpty(_accessToken))
            {
                return _accessToken;
            }

            // Prepare request data
            var clientId = _configuration["HighMobility:ClientId"];
            var clientSecret = _configuration["HighMobility:ClientSecret"];
            var authUrl = _configuration["HighMobility:OAUTH_TOKEN_URI"];

            var requestBody = new
            {
                client_id = clientId,
                client_secret = clientSecret,
                grant_type = "client_credentials"
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Send HTTP request to retrieve the token
            var response = await _httpClient.PostAsync(authUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get access token: {response.StatusCode}");
            }

            // Parse the response
            var responseData = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseData);

            // Cache the token and return it
            _accessToken = tokenResponse.AccessToken ?? "";

            return _accessToken;
        }

    }
}
