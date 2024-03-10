using System.Net;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class AppInfoRespository : IAppInfoRepository
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;

        public AppInfoRespository(DatabaseContext database , IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
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
    
                    return HttpStatusCode.NoContent;
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

        public async Task<object> ViewAllApps(Guid userId)
        {
            try
            {
                var appDetails = await this._database.AppInfo
                .Include(category => category.Category)
                .Include(review => review.AppReview)
                .Include(downloads => downloads.AppDownloads)
                .Where(status => status.Status == RequestStatus.Approved)
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

                        if(appInfo.AppDownloads.Where(id => id.UserId == userId).Any())
                        {
                            appInfoDto.UserDownloaded = true;
                        }
                        else
                        {
                            appInfoDto.UserDownloaded = false;
                        }
    
                        appDetailsDto[i] = appInfoDto;
                    }
    
                    return appDetailsDto;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
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
                    return HttpStatusCode.Found;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
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