using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppDownloadsRepository
    {
        Task<object> GetAppLogs(AppLogsDto appSearch);
    }
}