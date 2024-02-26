using System.Net;
using AutoMapper;
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
            var existedData = await this._database.AppInfo.FindAsync(id);

            if (existedData != null)
            {
                this._database.AppInfo.Remove(existedData);

                await this._database.SaveChangesAsync();
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<object> ViewAllApps()
        {
            var appDetails = await this._database.AppInfo
            .Include(category => category.Category)
            .Include(review => review.AppReview)
            .Include(downloads => downloads.AppDownloads)
            .ToListAsync();

            if (appDetails != null && appDetails.Count > 0)
            {
                // var appDetailsDto = new List<ListAppInfoDto>();

                // appDetails.ForEach(app =>
                // {

                //     var appDto = new ListAppInfoDto()
                //     {
                //         AppId = app.AppId,
                //         Name = app.Name,
                //         Logo = app.Logo,
                //         Description = app.Description,
                //         Rating = this.CalculateAverageRatings((List<AppReview>) app.AppReview),
                //         Downloads = app.AppDownloads.Count
                //     };

                //     appDetailsDto.Add(appDto);
                // });

                // return appDetailsDto;

                var appDetailsDto = this._mapper.Map<List<ListAppInfoDto>>(appDetails);

                int appsCount = Math.Min(appDetails.Count , appDetailsDto.Count);

                for(int i = 0; i < appsCount ; i++)
                {
                    AppInfo appInfo = appDetails[i];
                    ListAppInfoDto appInfoDto = appDetailsDto[i];

                    appInfoDto.Rating = this.CalculateAverageRatings((List<AppReview>)appInfo.AppReview);
                    appInfoDto.Downloads = appInfo.AppDownloads.Count;

                    appDetailsDto[i] = appInfoDto;
                }

                return appDetailsDto;
            }
            else
            {
                return HttpStatusCode.NoContent;
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