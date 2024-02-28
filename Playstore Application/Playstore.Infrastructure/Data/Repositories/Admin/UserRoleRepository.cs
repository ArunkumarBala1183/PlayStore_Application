using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        private readonly DatabaseContext database;

        private readonly RoleConfig role;
        public UserRoleRepository(DatabaseContext context, IOptions<RoleConfig> options) : base(context)
        {
            database = context;
            role = options.Value;
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
                        return await RoleUpdate(userDetails.UserId , appPublishDto.AppId);
                    }
                    else
                    {
                        return await RemoveRequest(userDetails.UserId);
                    }
                }
    
                return HttpStatusCode.NotFound;
            }
            catch (SqlException)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private async Task<HttpStatusCode> RoleUpdate(Guid userId , Guid appId)
        {
            var userRoles = new UserRole()
            {
                RoleId = role.DeveloperId,
                UserId = userId
            };

            Add(userRoles);

            await database.SaveChangesAsync();

            await this.PublishApp(appId);

            return HttpStatusCode.Created;
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

            if(appDetails != null)
            {
                appDetails.Status = RequestStatus.Approved;

                database.AppInfo.Update(appDetails);

                await database.SaveChangesAsync();
            }
        }

    }
}