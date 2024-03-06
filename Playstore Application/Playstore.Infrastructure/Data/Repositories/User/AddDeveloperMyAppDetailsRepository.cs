using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;


using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Contracts.DTO.AppDownloads;
using System.Linq;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AddDeveloperMyAppDetailsRespository : Repository<AppInfo>, IAppDeveloperMyAppDetailsRepository
    {
        public readonly DatabaseContext databaseContext;
        public AddDeveloperMyAppDetailsRespository(DatabaseContext context) : base(context)
        {
            databaseContext = context;

        }

        //Rendering AppDetails Using AppInfo Table
        public async Task<object> GetAppDetails(Guid UserId)
        {
            var response = await this.databaseContext.AppInfo
                .Include(data => data.AppDownloads)
                .Include(data => data.Category)
                .Where(obj => obj.UserId == UserId)
                .ToListAsync();
            int Count = response.Count();
            if (response.Any())
            {
                var myappDetails = response.Select(appInfo =>
                {
                    var appReview = this.databaseContext.AppReviews
                        .Where(review => review.AppId == appInfo.AppId)
                        .ToList();
                    var AppDownload = this.databaseContext.AppDownloads.Where(download => download.AppId == appInfo.AppId).ToList();

                    return new Myappdetails
                    {
                        AppId = appInfo.AppId,
                        Name = appInfo.Name,
                        Description = appInfo.Description,
                        Logo = appInfo.Logo,
                        PublishedDate = appInfo.PublishedDate,
                        PublisherName = appInfo.PublisherName,
                        Apps = Count,
                        Rating = appReview.Any() ? appReview.Average(review => review.Rating) : 0,
                        CategoryId = appInfo.Category.CategoryName,
                        Downloads = AppDownload.Count()
                    };
                }).ToList();

                return myappDetails;
            }

            throw new EntityNotFoundException($"No AppInfo found for UserId");
        }

    }
}

