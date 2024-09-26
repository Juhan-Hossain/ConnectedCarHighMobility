namespace Demo_APP.Interfaces
{
    public interface ICallHighMobilityService
    {
        Task CallAndProcessAsync(string vin);
        Task CallHighMobility(string vin);
    }
}