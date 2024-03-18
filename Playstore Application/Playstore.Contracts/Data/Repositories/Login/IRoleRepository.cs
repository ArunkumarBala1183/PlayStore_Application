using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<UserRole>> GetUserRolesAsync(Guid userId);
        Task<Role> GetByRoleId(Guid roleId);
        Task<Guid> GetDefaultRoleId();
        
    }
}