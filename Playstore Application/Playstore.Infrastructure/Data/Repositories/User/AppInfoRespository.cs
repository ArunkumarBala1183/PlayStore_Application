using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;


using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppInfoRespository : Repository<AppInfo>, IAppValueRepository
    {
        private readonly DatabaseContext context;
        public AppInfoRespository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<AppData> GetAppData(AppDownloadsDto appDownloadsDto)
        {
            var response = await context.AppInfo
            .Include(data => data.AppData)
            .FirstOrDefaultAsync(appId => appId.AppId == appDownloadsDto.AppId);
 
            if (response != null)
            {
                var fileEntity = this.context.AppDownloads.FirstOrDefault(app => app.AppId == appDownloadsDto.AppId && app.UserId == appDownloadsDto.UserId);
 
                if (fileEntity == null)
                {
                    var entity = new AppDownloads
                    {
                        AppId = appDownloadsDto.AppId,
                        UserId = appDownloadsDto.UserId,
                        DownloadedDate = DateTime.Today,
 
                    };
                    this.context.AppDownloads.Add(entity);
                    await this.context.SaveChangesAsync();
                    return response.AppData;
                    
                }
                throw new InvalidRequestBodyException();
            }
 
            throw new EntityNotFoundException($"No App Found");
        }
        public async Task<object> GetValue(Guid id)
        {
            var response = await context.AppInfo.FirstOrDefaultAsync(appId => appId.UserId == id);

            if (response != null)
            {
                return response;
            }
            return HttpStatusCode.NoContent;
        }
        public async Task<object> ViewAllApps()
        {
            var enetities = await this.context.AppInfo.Include(data => data.AppReview).Include(data => data.Category).Include(data => data.AppImages).Where(status => status.Status == RequestStatus.Approved).ToListAsync();
            int count = enetities.Count();
            if (enetities.Any())
            {
                var myappDetails = enetities.Select(appInfo =>
                 {
                     var appReview = this.context.AppReviews
                         .Where(review => review.AppId == appInfo.AppId)
                         .ToList();
                     var AppDownload = this.context.AppDownloads.Where(download => download.AppId == appInfo.AppId).ToList();

                     return new AppsdetailsDTO
                     {
                         AppId = appInfo.AppId,
                         Name = appInfo.Name,
                         Description = appInfo.Description,
                         Logo = appInfo.Logo,
                         UserId = appInfo.UserId,
                         Apps = count,
                         CategoryName = appInfo.Category.CategoryName,
                         Rating = appReview.Any() ? appReview.Average(review => review.Rating) : 0,
                         CategoryId = appInfo.Category.CategoryId,
                         Downloads = AppDownload.Count(),
                         Status = appInfo.Status
                     };
                 }).ToList();

                Console.WriteLine(myappDetails[0].Status);
                return myappDetails;

            }
            throw new EntityNotFoundException($"No Apps found");
        }


    }
}