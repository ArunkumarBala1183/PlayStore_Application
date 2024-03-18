using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Playstore.Core.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        public RoleRepository(DatabaseContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }
    
        public async Task<List<UserRole>> GetUserRoles(Guid userId)
        {
            return await _context.UserRole
                .Include(role => role.Role)
                .Where(user => user.UserId == userId)
                .ToListAsync();
        }

        public async Task<Guid> GetDefaultRoleId()
        {
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleCode == _configuration.GetValue<string>("RoleConfig:UserCode"));
            return defaultRole != null ? defaultRole.RoleId : Guid.Empty;
        }

        
    }
}
