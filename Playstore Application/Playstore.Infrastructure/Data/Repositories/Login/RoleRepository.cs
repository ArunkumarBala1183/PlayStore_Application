using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DatabaseContext _context;
        private readonly ILogger logger;
        public RoleRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            _context = context;
            logger = Log//.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(RoleRepository).Name);
        }
        public async Task<UserCredentials?> GetByEmailAsync(string email)
        {
            var response = await _context.UserCredentials.FirstOrDefaultAsync(mailid => mailid.EmailId == email);
            logger.Information($"UserCredentials Fetched for {email}");
            return response;
        }
        public async Task<List<UserRole>> GetUserRolesAsync(Guid userId)
        {
            var response = await _context.UserRole
                .Include(role => role.Role)
                .Where(user => user.UserId == userId)
                .ToListAsync();
            
            logger.Information("UserRoles Fetched from Server");

            return response;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> GetDefaultRoleId()
        {
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleCode == "User");
            logger.Information("Role Id Fetched from Server");
            return defaultRole != null ? defaultRole.RoleId : Guid.Empty;
        }


        public async Task<Role?> GetByRoleCode(string roleCode)
        {
            var response = await _context.Roles.FirstOrDefaultAsync(role => role.RoleCode == roleCode);
            logger.Information("Role fetched from Server");
            return response;
        }
        public async Task<Role?> GetByRoleId(Guid roleId)
        {
            var response =  await _context.Roles.FirstOrDefaultAsync(role => role.RoleId == roleId);
            logger.Information("Role fetched from the Server");
            return response;
        }
    }
}
