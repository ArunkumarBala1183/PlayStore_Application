using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppRequests;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppRequestsRepository : IAppRequestsRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        public AppRequestsRepository(DatabaseContext context , IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
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