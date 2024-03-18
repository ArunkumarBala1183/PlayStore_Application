using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<UserRole>> GetUserRoles(Guid userId);
        Task<Guid> GetDefaultRoleId();
        
    }
}