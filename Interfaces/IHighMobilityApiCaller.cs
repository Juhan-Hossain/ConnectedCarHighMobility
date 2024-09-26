namespace Demo_APP.Interfaces
{
    public interface IHighMobilityApiCaller
    {
        Task<object?> CallApiAsync(string endpoint);
    }
}