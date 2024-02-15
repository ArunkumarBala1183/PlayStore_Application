using Playstore.Contracts.Data.Repositories;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
        IAppRepository App { get; }
        IUserRepository User { get; }
        Task CommitAsync();
    }
}