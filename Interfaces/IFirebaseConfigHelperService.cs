using Demo_APP.Helpers;

namespace Demo_APP.Interfaces
{
    public interface IFirebaseConfigHelperService
    {
        Task WriteConfigAsync(FirebaseAppConfigRoot config);
        Task UpdateDeviceRegistrationTokenAsync(string newToken);
        Task<FirebaseAppConfigRoot?> ReadConfigAsync();
    }
}
