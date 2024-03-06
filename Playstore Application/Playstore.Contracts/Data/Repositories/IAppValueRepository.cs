using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppValueRepository : IRepository<AppInfo>
    {
        Task<object> GetAppData(AppDownloadsDto appDownloadsDto);
        Task<object> GetValue(Guid id);
       Task<object>ViewAllApps();
    }
}