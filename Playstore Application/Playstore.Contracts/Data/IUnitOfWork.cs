using Playstore.Contracts.Data.Repositories;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }

        IUserRoleRepository UserRole {get;}
        
        Task CommitAsync();
    }
}