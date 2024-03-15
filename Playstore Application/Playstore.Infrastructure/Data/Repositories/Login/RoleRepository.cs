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
        public RoleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        public async Task<UserCredentials?> GetByEmailAsync(string email)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(mailid => mailid.EmailId == email);
        }
        public async Task<List<UserRole>> GetUserRolesAsync(Guid userId)
        {
            return await _context.UserRole
                .Include(role => role.Role)
                .Where(user => user.UserId == userId)
                .ToListAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> GetDefaultRoleId()
        {
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleCode == "User");
            return defaultRole != null ? defaultRole.RoleId : Guid.Empty;
        }


        public async Task<Role?> GetByRoleCode(string roleCode)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.RoleCode == roleCode);
            
        }
        public async Task<Role?> GetByRoleId(Guid roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.RoleId == roleId);
        
        }
    }
}
