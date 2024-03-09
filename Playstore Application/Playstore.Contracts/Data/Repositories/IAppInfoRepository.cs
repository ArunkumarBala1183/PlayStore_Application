using System.Net;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppInfoRepository
    {
        Task<object> ViewAllApps();

        Task<HttpStatusCode> RemoveApp(Guid id);
        Task<HttpStatusCode> GetUserDownloadedOrNot(Guid userId , Guid appId);
    }
}