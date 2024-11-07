using Demo_APP.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Demo_APP.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IFirebaseConfigHelperService _firebaseConfigHelperService;
        private string _registrationToken = "";
        public PushNotificationService(IFirebaseConfigHelperService firebaseConfigHelperService)
        {
            _firebaseConfigHelperService = firebaseConfigHelperService;
            _registrationToken = _firebaseConfigHelperService.ReadConfigAsync().Result.FirebaseApp.DeviceRegistrationToken;
        }

        public async Task SendPushNotification(double temperature)
        {
            try
            {

                var notificationPayload = new Message
                {
                    Token = _registrationToken,
                    Notification = new Notification
                    {
                        Title = "High Engine Coolant Temperature",
                        Body = $"Your car's engine's temperature overheated to {temperature} °C, which is above the safe threshold (45°C)."
                    }
                };
                var jsonPayload = JsonConvert.SerializeObject(notificationPayload);
                string response1 = await FirebaseMessaging.DefaultInstance.SendAsync(notificationPayload);
                
            }
            catch (TaskCanceledException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
