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
        private readonly string _registrationToken = "faDfOof2Rjyh3a4hy5NCfc:APA91bGE3IlGUtSyFfj4DkQmvn8OebpyzDgevcsWEyzEuRJrrJWY3m-zJnaQjSp6zD2JdV2RYUuRcU_h4GkF_mdoFcTwOEYdBv4DHWo8lLSmdkfwsEqYiHmkry8G_VguSDduCrshy8Lp";

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
                        Body = $"The engine coolant temperature is {temperature} °C, which is above the safe threshold."
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
