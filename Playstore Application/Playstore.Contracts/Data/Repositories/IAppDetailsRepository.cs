using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppDetailsRepository : IRepository<AppInfo>
    {
     Task<object> GetAppDetails(Guid AppId);
    }
}