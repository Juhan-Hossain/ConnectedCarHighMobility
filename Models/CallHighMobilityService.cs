using Demo_APP.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo_APP.Models
{
    public class CallHighMobilityService : ICallHighMobilityService
    {
        private readonly IHighMobilityApiCaller _highMobilityApiCaller;
        private readonly IMemoryCache _cache;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IConfiguration configuration;
        private readonly string _baseUrl = "https://sandbox.api.high-mobility.com/";
        private string _acessToken = "";
        private FirebaseApp _firebaseApp;

        public CallHighMobilityService(
            IHighMobilityApiCaller highMobilityApiCaller,
            IMemoryCache memoryCache,
            IPushNotificationService pushNotificationService,
            IHighMobilityAuthService highMobilityAuthService,
            IConfiguration configuration)

        {
            _highMobilityApiCaller = highMobilityApiCaller;
            _cache = memoryCache;
            _pushNotificationService = pushNotificationService;
            this.configuration = configuration;

            // Ensure security protocols
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12 |
                System.Net.SecurityProtocolType.Tls13;

            // Initialize Firebase only if it hasn't been initialized already
            if (FirebaseApp.DefaultInstance == null)
            {
                lock (this) // Ensure thread safety in case of multiple calls
                {
                    if (_firebaseApp == null)
                    {
                        string serviceAccountPath = configuration["FirebaseApp:AppAdminPath"];

                        // Initialize Firebase app with the service account
                        _firebaseApp = FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile(serviceAccountPath),
                        });
                    }
                }
            }

            // Get the access token from Firebase credentials (can be done on demand later)
            var googleCredential = GoogleCredential.FromFile(configuration["FirebaseApp:AppAdminPath"])
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            var accessToken = googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }
        public async Task CallAndProcessAsync(string vin)
        {
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddHours(10);
            //var cacheKey = $"vehicleData_{vin}_0";
            //var cacheDuration = TimeSpan.FromHours(2);
            //_cache.Set(cacheKey, endTime);
            try
            {
                while (DateTime.UtcNow <= endTime)
                {
                    var vehicleData = _highMobilityApiCaller.CallApiAsync($"{_baseUrl}/v1/vehicle-data/autoapi-13/{vin}");
                    var result = vehicleData?.Result?.ToString() ?? "";

                    var myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
                    var response = myDeserializedClass?.diagnostics.engine_coolant_temperature.data.value.ToString() ?? "";


                    var engineCoolantTemperature = myDeserializedClass?.diagnostics.engine_coolant_temperature.data.value ?? 0;

                    // Check if temperature is greater than 45°C
                    if (engineCoolantTemperature > 45)
                    {
                        // Send push notification immediately
                        await _pushNotificationService.SendPushNotification(engineCoolantTemperature);
                        //break; // Exit the loop since we've triggered the notification
                    }

                    // Wait for 0.5 seconds before the next API call
                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task CallHighMobility(string vin)
        {
            var vehicleData = _highMobilityApiCaller.CallApiAsync($"{_baseUrl}/v1/vehicle-data/autoapi-13/{vin}");
            var result = vehicleData?.Result?.ToString() ?? "";

            var myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
            var response = myDeserializedClass?.diagnostics.engine_coolant_temperature.data.value.ToString() ?? "";
            await _pushNotificationService.SendPushNotification(double.Parse(response));
        }
    }
}
