using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DatabaseContext _context;
        //private readonly DbSet<Users> _dbSet;
        public RoleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            // _dbSet = _context.Set<Users>();
        }
        public async Task<UserCredentials> GetByEmailAsync(string email)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(x => x.EmailId == email);
        }
        public async Task<List<UserRole>> GetUserRolesAsync(Guid userId)
        {
            return await _context.UserRole
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
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

        public async Task<Guid> GetDefaultRoleId()
        {
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == "User");
            return defaultRole != null ? defaultRole.RoleId : Guid.Empty;
        }
    

        public async Task<Role> GetByRoleCode(string roleCode)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode);
        }
        public async Task<Role> GetByRoleId(Guid roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        }

        
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
