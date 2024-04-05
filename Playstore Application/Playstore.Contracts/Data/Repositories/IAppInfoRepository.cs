using System.Net;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppInfoRepository
    {
        object ViewAllApps(Guid userId);

        Task<HttpStatusCode> RemoveApp(Guid id);
        Task<HttpStatusCode> GetUserDownloadedOrNot(Guid userId , Guid appId);
    }
}