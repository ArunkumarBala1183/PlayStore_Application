using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories
{
    public class AppInfoRespository : IAppInfoRepository
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;
        private readonly ILogger logger;

        public AppInfoRespository(DatabaseContext database , IMapper mapper , IHttpContextAccessor httpContext)
        {
            this._database = database;
            this._mapper = mapper;
            logger = Log.ForContext("userId" , httpContext.HttpContext?.Items["userId"]?.ToString())
                        .ForContext("Location" , typeof(AppInfoRespository).Name);
        }

        public async Task<HttpStatusCode> RemoveApp(Guid id)
        {
            try
            {
                var existedData = await this._database.AppInfo.FindAsync(id);
    
                if (existedData != null)
                {
                    this._database.AppInfo.Remove(existedData);
    
                    await this._database.SaveChangesAsync();
                    
                    logger.Information($"{existedData.Name} Deleted App Successfully in Database");

                    return HttpStatusCode.NoContent;
                }

                logger.Information($"AppId : {id} Not Found in Database");
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

        public async Task<object> ViewAllApps(Guid userId)
        {
            try
            {
                var appDetails = await this._database.AppInfo
                .Include(category => category.Category)
                .Include(review => review.AppReview)
                .Include(downloads => downloads.AppDownloads)
                .Where(status => status.Status == RequestStatus.Approved)
                .AsSplitQuery()
                .ToListAsync();
    
                if (appDetails != null && appDetails.Count > 0)
                {
    
                    var appDetailsDto = this._mapper.Map<List<ListAppInfoDto>>(appDetails);
    
                    int appsCount = Math.Min(appDetails.Count , appDetailsDto.Count);
    
                    for(int i = 0; i < appsCount ; i++)
                    {
                        AppInfo appInfo = appDetails[i];
                        ListAppInfoDto appInfoDto = appDetailsDto[i];
    
                        appInfoDto.Rating = this.CalculateAverageRatings((List<AppReview>)appInfo.AppReview);
                        appInfoDto.Downloads = appInfo.AppDownloads.Count;

                        if(appInfo.AppDownloads.Any(id => id.UserId == userId))
                        {
                            appInfoDto.UserDownloaded = true;
                        }
                        else
                        {
                            appInfoDto.UserDownloaded = false;
                        }
    
                        appDetailsDto[i] = appInfoDto;
                    }

                    logger.Information("Apps Fetched from Database");
                
                    return appDetailsDto;
                }
                else
                {
                    logger.Information("No Apps Found in Database");
                    return HttpStatusCode.NotFound;
                }
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

        public async Task<HttpStatusCode> GetUserDownloadedOrNot(Guid userId , Guid appId)
        {
            try
            {
                var appDetails = await this._database.AppInfo
                .Include(downloads => downloads.AppDownloads)
                .Where(downloads => downloads.AppDownloads.Any(user => user.UserId == userId && user.AppId == appId))
                .FirstOrDefaultAsync();
    
                if (appDetails != null)
                {
                    logger.Information($"Apps Downloaded by UserId - {userId} had Fetched");
                    return HttpStatusCode.Found;
                }
                else
                {
                    logger.Information($"No Apps Downloaded by UserId - {userId}");
                    return HttpStatusCode.NotFound;
                }
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

        private int CalculateAverageRatings(List<AppReview> appReviews)
        {
            if(appReviews != null && appReviews.Count > 0)
            {
                int totalRatings = 0;
                int totalCount = appReviews.Count;

                appReviews.ForEach(review => {
                    totalRatings += review.Rating;
                });

                return totalRatings/totalCount;
            }

            return 0;
        }
    }
}