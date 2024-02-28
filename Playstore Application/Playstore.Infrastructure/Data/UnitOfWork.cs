using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IAppRepository App => new AppRepository(_context);

        public IUserRepository User => new UserRepository(_context);
        //public IUsersRepository Users => new UsersRepository(_context);
        //public IUserCredentialsRepository UserCredentials => new UserCredentialsRe(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}