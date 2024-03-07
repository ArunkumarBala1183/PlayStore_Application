using System.Net;
using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
     public interface IAppInfoRepository:IRepository<AppInfo>
    {
        Task<object> ViewAllApps();

        Task<HttpStatusCode> RemoveApp(Guid id);
        
    }
}