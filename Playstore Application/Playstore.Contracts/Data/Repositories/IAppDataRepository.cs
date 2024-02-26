using Microsoft.AspNetCore.Http;
using Playstore.Contracts.DTO.AppData;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppDataRepository
    {
        Task<object> GetAppData(Guid appId);

        Task<object> UploadApp(IFormFile appFile , Guid appId);
    }
}