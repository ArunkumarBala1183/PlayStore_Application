using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
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
        public AppInfoDownloadRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            this.databaseContext = context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(AppInfoDownloadRepository).Name);
        }

        public async Task<object> GetData(Guid Userid)
        {
            var response = await databaseContext.AppDownloads
            .Include(data => data.AppInfo)
            .Include(data => data.AppInfo.Category)
            .Where(userId => userId.UserId == Userid)
            .ToListAsync();
            if (response.Count > 0)
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
                        Rating = appReview.Any() ? appReview.Average(review => review.Rating) : 0,
                        Category = appInfo.AppInfo.Category.CategoryName,
                        Downloads = AppDownload.Count,
                    };
                })
                .ToList();

                logger.Information($"AppDownloads fetched for id {Userid}");
                return myappDetails;

            }
            var message = "No AppDownloads found";
            logger.Information(message);
            throw new EntityNotFoundException(message);
        }

    }
}