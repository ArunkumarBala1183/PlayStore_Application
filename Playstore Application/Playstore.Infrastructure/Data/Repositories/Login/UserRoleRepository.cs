using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly DatabaseContext _context;
        public UserRoleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
