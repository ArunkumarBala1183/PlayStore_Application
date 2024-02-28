using Playstore.Contracts.Data.Repositories;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }
        //IUsersRepository  Users { get; }
        //IUserCredentialsRepository  UserCredentials { get; }
        Task CommitAsync();
    }
}