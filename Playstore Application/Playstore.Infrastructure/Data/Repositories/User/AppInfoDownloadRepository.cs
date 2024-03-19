using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Utility;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppInfoDownloadRepository : Repository<AppDownloads>, IAppInfoDownloadRepository
    {
        public readonly DatabaseContext databaseContext;
        private readonly ILogger logger;
        public AppInfoDownloadRepository(DatabaseContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.databaseContext = context;
            logger = Log.ForContext(Dataconstant.UserId, httpContext.HttpContext?.Items[Dataconstant.UserId])
                        .ForContext(Dataconstant.Location, typeof(AppInfoDownloadRepository).Name);
        }

        public async Task<object> GetData(Guid Userid)
        {
            try
            {
                var response = await databaseContext.AppDownloads
                .Include(data => data.AppInfo)
                .Include(data => data.AppInfo.Category)
                .Where(userId => userId.UserId == Userid)
                .ToListAsync();
                if (response.Count > Dataconstant.ResponseCount)
                {
                    var myappDetails = response.Select(appInfo =>
                    {
                        var appReview = this.databaseContext.AppReviews
                            .Where(review => review.AppId == appInfo.AppId)
                            .ToList();
                        var AppDownload = this.databaseContext.AppDownloads.Where(download => download.AppId == appInfo.AppId).ToList();

                        return new AppStoreDTO
                        {
                            AppId = appInfo.AppId,
                            FileName = appInfo.AppInfo.Name,
                            Logo = appInfo.AppInfo.Logo,
                            Description = appInfo.AppInfo.Description,
                            Rating = appReview.Any() ? appReview.Average(review => review.Rating) : Dataconstant.NullRating,
                            Category = appInfo.AppInfo.Category.CategoryName,
                            Downloads = AppDownload.Count,
                        };
                    }).ToList();

                    logger.Information(Dataconstant.AppDownloadsuccessinfo+Dataconstant.Singlespace+Userid);
                    return myappDetails;

                }
                var message = Dataconstant.NoAppFoundInfo;
                logger.Information(message);
                throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);


            }


            catch (SqlException exception)
            {
                throw new SqlException($"{exception}");
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }
    }
}


