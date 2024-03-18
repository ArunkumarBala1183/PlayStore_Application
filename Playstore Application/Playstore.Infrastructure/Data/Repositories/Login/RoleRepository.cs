using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Playstore.Core.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger logger;
        public RoleRepository(DatabaseContext context, IConfiguration configuration , IHttpContextAccessor httpContext) : base(context)
        {
            _context = context;
            _configuration = configuration;
            logger = Log//.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(RoleRepository).Name);
        }
    
        public async Task<List<UserRole>> GetUserRoles(Guid userId)
        {
            var response = await _context.UserRole
                .Include(role => role.Role)
                .Where(user => user.UserId == userId)
                .ToListAsync();
            return response;
        }

        public async Task<Guid> GetDefaultRoleId()
        {
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleCode == "User");
            logger.Information("Role Id Fetched from Server");
            return defaultRole != null ? defaultRole.RoleId : Guid.Empty;
        }


        
    }
}
