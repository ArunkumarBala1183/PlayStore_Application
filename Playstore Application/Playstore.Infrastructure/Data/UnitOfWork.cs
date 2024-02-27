using Microsoft.Extensions.Options;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Core.Data.Repositories;
using Playstore.Core.Data.Repositories.Admin;
using Playstore.Migrations;

namespace Playstore.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private readonly IOptions<RoleConfig> roleConfig;

        public UnitOfWork(DatabaseContext context , IOptions<RoleConfig> roleConfig)
        {
            _context = context;
            this.roleConfig = roleConfig;
        }
        public IAppRepository App => new AppRepository(_context);

        public IUserRepository User => new UserRepository(_context);

        public IUserRoleRepository UserRole => new UserRoleRepository(_context , roleConfig);
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}