using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppValueRepository : IRepository<AppInfo>
    {
        Task<object> GetAppData(Guid id);
        Task<object> GetValue(Guid id);
        Task<object>ViewAllApps();
    }
}