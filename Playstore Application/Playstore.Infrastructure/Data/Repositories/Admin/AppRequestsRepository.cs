using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppRequests;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppRequestsRepository : IAppRequestsRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        public AppRequestsRepository(DatabaseContext context , IMapper mapper , IHttpContextAccessor httpContext)
        {
            this.database = context;
            this.mapper = mapper;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(ApplicationLogsRepository).Name);
        }

        public async Task<object> GetAllRequests()
        {
            try
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
                    logger.Information("App Requests Fetched from Server");
                    return requestDetailsDto;
                }

                logger.Information("No Application Request Found in Server");
                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch(Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<object> GetRequestedAppDetails(Guid appId)
        {
            try
            {
                var appDetails = await this.database.AppInfo
                .Include(user => user.Users)
                .Include(category => category.Category)
                .Include(appImages => appImages.AppImages)
                .Include(appData => appData.AppData)
                .AsSplitQuery()
                .FirstOrDefaultAsync(id => id.AppId == appId);
    
                if(appDetails != null)
                {
                    var appDetailsDto = this.mapper.Map<RequestAppInfoDto>(appDetails);
                    
                    logger.Information($"Requested Details Fetched for AppId {appId}");
                    return appDetailsDto;
                }
                
                logger.Information($"Requested Details not found for AppId {appId}");

                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch(Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}