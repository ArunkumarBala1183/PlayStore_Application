using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Contracts.DTO.AppRequests;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppRequestsRepository : IAppRequestsRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;

        private readonly RoleConfig roleConfig;
        public AppRequestsRepository(DatabaseContext context , IMapper mapper, IOptions<RoleConfig> options)
        {
            this.database = context;
            this.mapper = mapper;
            this.roleConfig = options.Value;
        }

        public async Task<object> ApproveApp(AppPublishDto appPublishDto)
        {
            if(appPublishDto.Approve)
            {
                return await this.RoleUpdate(appPublishDto.AppId);
            }
            else
            {
                return await this.RemoveAppInfo(appPublishDto.AppId);
            }
        }

        private async Task<object> RoleUpdate(Guid appId)
        {
            var userDetails = await this.database.Users
            .Include(app => app.AppInfo)
            .Include(roles => roles.UserRoles)
            .FirstOrDefaultAsync(id => id.AppInfo.Any(id => id.AppId == appId));

            if (userDetails != null)
            {
                try
                {
                    var userRoles = new UserRole()
                    {
                        RoleId = this.roleConfig.DeveloperId,
                        UserId = userDetails.UserId
                    };
    
                    userDetails.UserRoles.Add(userRoles);
    
                    await this.database.SaveChangesAsync();
    
                    return HttpStatusCode.Created;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

            return HttpStatusCode.NotFound;
        }

        private async Task<HttpStatusCode> RemoveAppInfo(Guid appId)
        {
            var appDetails = await this.database.AppInfo.FindAsync(appId);

            if(appDetails != null)
            {
                this.database.AppInfo.Remove(appDetails);
                
                await this.database.SaveChangesAsync();

                return HttpStatusCode.Created;
            }

            return HttpStatusCode.NotFound;
        }
        

        public async Task<object> GetAllRequests()
        {
            var requests = await this.database.AdminRequests
            .Include(userInfo => userInfo.Users)
            .Include(appInfo => appInfo.AppInfo)
            .ThenInclude(category => category.Category)
            .Where(status => status.AppInfo.Status == RequestStatus.Pending)
            .ToListAsync();

            if(requests != null && requests.Count > 0)
            {
                var requestDetailsDto = new List<RequestDetailsDto>();

                requests.ForEach(request => {

                    var requestDetails = new RequestDetailsDto()
                    {
                        AppId = request.AppInfo.AppId,
                        Name = request.AppInfo.Name,
                        Description = request.AppInfo.Description,
                        Logo = request.AppInfo.Logo,
                        Category = request.AppInfo.Category.CategoryName
                    };

                    requestDetailsDto.Add(requestDetails);
                });

                return requestDetailsDto;
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<object> GetRequestedAppDetails(Guid appId)
        {
            var appDetails = await this.database.AppInfo
            .Include(user => user.Users)
            .Include(category => category.Category)
            .Include(appImages => appImages.AppImages)
            .Include(appData => appData.AppData)
            .FirstOrDefaultAsync(id => id.AppId == appId);

            if(appDetails != null)
            {
                var appDetailsDto = this.mapper.Map<RequestAppInfoDto>(appDetails);
                
                return appDetailsDto;
            }

            return HttpStatusCode.NoContent;
        }
    }
}