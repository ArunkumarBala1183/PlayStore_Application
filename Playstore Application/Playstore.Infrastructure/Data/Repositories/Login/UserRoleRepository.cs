using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly DatabaseContext _context;
        //private readonly DbSet<Users> _dbSet;
        public UserRoleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            // _dbSet = _context.Set<Users>();
        }
        // public async Task<List<Users>> GetAll()
        // {
        //     return await _context.Users.ToListAsync();
        // }

        //public IUsersRepository Users => new UsersRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        // public async Task<UserRole> GetByUserId(Guid userId)
        // {
        //     return await _context.user.FirstOrDefaultAsync(r => r. == userId);
        // }
        // public async Task<Users> GetByEmailId(string emailId)
        // {
        //     return await _context.Users.FirstOrDefaultAsync(u => u.EmailId == emailId);
        // }
        // public async Task<Users> GetByPhoneNumber(string mobileNumber)
        // {
        //     return await _context.Users.FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber);
        // }
    }
}
