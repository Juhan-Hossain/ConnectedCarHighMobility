namespace Demo_APP.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendPushNotification(double temperature);
    }
}