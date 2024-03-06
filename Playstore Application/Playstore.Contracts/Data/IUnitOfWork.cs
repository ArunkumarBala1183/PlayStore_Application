using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Repositories.Admin;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }

        IDeveloperRole UserRole {get;}
        
        Task CommitAsync();
    }
}