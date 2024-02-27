using System.Net;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppFilesRepository : IRepository<AppInfo> { 
        Task<HttpStatusCode> AddFiles(CreateAppInfoDTO createAppInfoDTO);
    }
}