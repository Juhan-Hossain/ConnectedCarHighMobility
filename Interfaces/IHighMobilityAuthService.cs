namespace Demo_APP.Interfaces
{
    public interface IHighMobilityAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}
