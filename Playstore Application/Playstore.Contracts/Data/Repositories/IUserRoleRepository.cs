using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppPublishRequest;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<object> ApproveApp(AppPublishDto appPublishDto);
    }
}