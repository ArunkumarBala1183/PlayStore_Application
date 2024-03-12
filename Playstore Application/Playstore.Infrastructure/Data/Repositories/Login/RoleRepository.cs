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
        public async Task<UserCredentials> GetByEmailAsync(string email)
        {
            var emailid = await _context.UserCredentials.FirstOrDefaultAsync(x => x.EmailId == email);
            if (emailid == null)
            {
                throw new Exception("Email is null");
            }
            return emailid;
        }
        public async Task<List<UserRole>> GetUserRolesAsync(Guid userId)
        {
            return await _context.UserRole
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

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
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode == roleCode);
            if (role == null)
            {
                throw new Exception("Role is null");
            }
            return role;
        }
        public async Task<Role> GetByRoleId(Guid roleId)
        {
            var roleid = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
            if (roleid == null)
            {
                throw new Exception("Role id is null");
            }
            return roleid;
        }
    }
}
