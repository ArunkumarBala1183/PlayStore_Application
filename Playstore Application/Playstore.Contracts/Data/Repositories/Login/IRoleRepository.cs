using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task CommitAsync();
        Task<List<UserRole>> GetUserRolesAsync(Guid userId);
        Task<Role> GetByRoleCode(string roleCode);
        Task<Role> GetByRoleId(Guid roleId);
        Task<UserCredentials> GetByEmailAsync(string email);
        Task<Guid> GetDefaultRoleId();
        
    }
}