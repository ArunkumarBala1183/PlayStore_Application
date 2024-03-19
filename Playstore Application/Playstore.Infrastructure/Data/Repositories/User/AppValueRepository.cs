using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using SqlException = Playstore.Core.Exceptions.SqlException;
using Playstore.Contracts.Data.Utility;
using Serilog;
using Microsoft.AspNetCore.Http;


namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppValueRepository : Repository<AppInfo>, IAppValueRepository
    {
        private readonly DatabaseContext context;
        private readonly ILogger logger;
        public AppValueRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            this.context = context;
            logger = Log.ForContext(Dataconstant.UserId, httpContext.HttpContext?.Items[Dataconstant.UserId])
                        .ForContext(Dataconstant.Location, typeof(AppValueRepository).Name);
        }

        public async Task<AppData> GetAppData(Guid appId , Guid userId)
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
                        UserId = userId,
                        DownloadedDate = DateTime.Today,
                    };
                    this.context.AppDownloads.Add(entity);
                    await this.context.SaveChangesAsync();
                    logger.Information(Dataconstant.AppDataFetchedInfo+Dataconstant.Singlespace+appId);
                    return response.AppData;
                    
                }
                logger.Information(Dataconstant.BadRequest);
                throw new InvalidRequestBodyException();
            }
            var message = Dataconstant.NoAppFound;
            logger.Information(message);
            throw new EntityNotFoundException(message);
        }
        public async Task<object> GetValue(Guid id)
        {
            try
            {
                var response = await context.AppInfo.FirstOrDefaultAsync(appId => appId.UserId == id);

            if (response != null)
            {
                logger.Information(Dataconstant.AppFetchedId+Dataconstant.Singlespace+id);
                return response;
            }
            logger.Information(Dataconstant.NoAppFetched+Dataconstant.Singlespace+id);
            return HttpStatusCode.NoContent;
                
            }
            catch(SqlException exception)
            {
                throw new SqlException($"{exception}");
            }
            
        }
        public async Task<object> ViewAllApps()
        {
            try
            {
                var enetities = await this.context.AppInfo.Include(data => data.AppReview).Include(data => data.Category).Include(data => data.AppImages).Where(status => status.Status == RequestStatus.Approved).ToListAsync();
                int count = enetities.Count;
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
                             Rating = appReview.Any() ? appReview.Average(review => review.Rating) : Dataconstant.NullRating,
                             CategoryId = appInfo.Category.CategoryId,
                             Downloads = AppDownload.Count,
                             Status = appInfo.Status
                         };
                     }).ToList();
                      logger.Information(Dataconstant.AppFetchedFromServer);
                    return myappDetails;
                  
               
                
            }
            var message = Dataconstant.NoAppFound;
            logger.Information(message);
            throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }

    }
}