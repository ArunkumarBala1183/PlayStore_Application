using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories.Admin;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class DeveloperRoleRepository : Repository<UserRole>, IDeveloperRole
    {
        private readonly DatabaseContext database;

        private readonly RoleConfig role;
        private readonly ILogger logger;
        public DeveloperRoleRepository(DatabaseContext context, IOptions<RoleConfig> options , IHttpContextAccessor httpContext) : base(context)
        {
            database = context;
            role = options.Value;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(UserRoleRepository).Name);
        }

        public async Task<HttpStatusCode> ApproveApp(AppPublishDto appPublishDto)
        {
            try
            {
                var userDetails = await database.Users
                .Include(app => app.AppInfo)
                .Include(roles => roles.UserRoles)
                .FirstOrDefaultAsync(id => id.AppInfo.Any(id => id.AppId == appPublishDto.AppId));

                if (userDetails != null)
                {
                    if (appPublishDto.Approve)
                    {
                        logger.Information("RoleUpdated");
                        return await RoleUpdate(userDetails.UserId, appPublishDto.AppId);
                    }
                    else
                    {
                        logger.Information("Request Rejected");
                        return await RemoveRequest(userDetails.UserId);
                    }
                }

                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        private async Task<HttpStatusCode> RoleUpdate(Guid userId, Guid appId)
        {
            var developerDetails = await this.database.Users
            .Include(userrole => userrole.UserRoles)
            .Where(id => id.UserId == userId)
            .FirstOrDefaultAsync();

            if (developerDetails != null)
            {
                var developerRole = developerDetails.UserRoles.FirstOrDefault(id => id.RoleId != role.DeveloperId);
                if (developerRole != null)
                {
                    developerRole.RoleId = role.DeveloperId;

                    Update(developerRole);

                    await database.SaveChangesAsync();
                }

                await this.PublishApp(appId);

                return HttpStatusCode.Created;
            }

            return HttpStatusCode.NotFound;
        }

        private async Task<HttpStatusCode> RemoveRequest(Guid userId)
        {
            var requestDetails = await database.AdminRequests.FirstOrDefaultAsync(id => id.UserId == userId);

            if (requestDetails != null)
            {
                database.AdminRequests.Remove(requestDetails);

                await database.SaveChangesAsync();

                return HttpStatusCode.NoContent;
            }

            return HttpStatusCode.NotFound;
        }

        private async Task PublishApp(Guid appId)
        {
            var appDetails = await database.AppInfo.FindAsync(appId);

            if (appDetails != null)
            {
                appDetails.Status = RequestStatus.Approved;
                appDetails.PublishedDate = DateTime.Now;

                database.AppInfo.Update(appDetails);

                await database.SaveChangesAsync();
            }
        }

    }
}