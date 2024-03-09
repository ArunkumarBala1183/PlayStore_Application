using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppDetailsRepository : IRepository<AppInfo>
    {
     Task<object> GetAppDetails(Guid id,Guid userId);
    }
}