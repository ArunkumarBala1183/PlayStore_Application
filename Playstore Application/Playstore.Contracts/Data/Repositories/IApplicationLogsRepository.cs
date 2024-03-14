namespace Playstore.Contracts.Data.Repositories
{
    public interface IApplicationLogsRepository
    {
        Task<object> ViewLogs();
    }   
}