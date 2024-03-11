using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task CommitAsync();
    }
}