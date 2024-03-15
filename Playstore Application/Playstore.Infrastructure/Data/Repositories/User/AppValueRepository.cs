using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;


namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppValueRepository : Repository<AppInfo>, IAppValueRepository
    {
        private readonly DatabaseContext context;
        public AppValueRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<AppData> GetAppData(Guid appId, Guid userId)
        {
            try
            {
                var response = await context.AppInfo
                .Include(data => data.AppData)
                .FirstOrDefaultAsync(id => id.AppId == appId);

                if (response != null)
                {
                    var fileEntity = this.context.AppDownloads.FirstOrDefault(app => app.AppId == appId && app.UserId == userId); //use userId here

                    if (fileEntity == null)
                    {
                        var entity = new AppDownloads
                        {
                            AppId = appId,
                            UserId = userId, //use userId here
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
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }
        public async Task<object> GetValue(Guid id)
        {
            try
            {
                var response = await context.AppInfo.FirstOrDefaultAsync(appId => appId.UserId == id);

                if (response != null)
                {
                    return response;
                }
                return HttpStatusCode.NoContent;
            }
            catch(SqlException exception)
            {
                throw new Exception($"{exception}");
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }
        public async Task<object> ViewAllApps()
        {
            try
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
                    return myappDetails;

                }
                throw new EntityNotFoundException($"No Apps found");
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }

    }
}