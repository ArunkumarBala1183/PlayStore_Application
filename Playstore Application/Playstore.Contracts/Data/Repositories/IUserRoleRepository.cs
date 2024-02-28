using System.Net;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppPublishRequest;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<HttpStatusCode> ApproveApp(AppPublishDto appPublishDto);
    }
}