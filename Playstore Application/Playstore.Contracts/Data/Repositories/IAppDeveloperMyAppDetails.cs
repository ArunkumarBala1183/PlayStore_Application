using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppDeveloperMyAppDetailsRepository : IRepository<AppInfo> { 
        Task<object> GetAppDetails(Guid UserId);
        
    }
}