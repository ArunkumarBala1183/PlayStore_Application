using System.Net;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppPublishRequest;

namespace Playstore.Contracts.Data.Repositories.Admin
{
    public interface IDeveloperRole : IRepository<UserRole>
    {
        Task<HttpStatusCode> ApproveApp(AppPublishDto appPublishDto);
    }
}