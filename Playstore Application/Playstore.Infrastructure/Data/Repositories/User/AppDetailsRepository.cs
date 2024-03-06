using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppDetailsRepository : Repository<AppInfo>, IAppDetailsRepository
    {
        public readonly DatabaseContext databaseContext;
        public AppDetailsRepository(DatabaseContext context) : base(context)
        {
            this.databaseContext = context;
        }
        //Getting AppDetails Using AppInfo,AppImages,AppReviews and AppDownloads Tables
        public async Task<object> GetAppDetails(Guid AppId)
        {
            var response = await this.databaseContext.AppInfo.Include(obj => obj.Category).Include(obj => obj.AppImages).Where(obj => obj.AppId == AppId).Where(status => status.Status == RequestStatus.Pending || status.Status == RequestStatus.Approved).ToListAsync();

            var appImages = await this.databaseContext.AppImages.Where(obj => obj.AppId == AppId).ToListAsync();
            var AppRating = await this.databaseContext.AppReviews.Where(obj => obj.AppId == AppId).ToListAsync();
            var Value = await this.databaseContext.AppDownloads.Where(obj => obj.AppId == AppId).ToListAsync();
            int Count = Value.Count();
            int Totalvalue = response.Count();
            if (response.Any())
            {
                var appinfoDetails = response.Select(response => new AppInfoDetailsDTO
                {
                    appImages = appImages.Select(image => image.Image).ToList(),
                    AppId = response.AppId,
                    Name = response.Name,
                    Description = response.Description,
                    Logo = response.Logo,
                    UserId = response.UserId,
                    PublishedDate = response.PublishedDate,
                    Publishername = response.PublisherName,
                    Apps = Totalvalue,
                    Rating = AppRating.Any() ? AppRating.Average(review => review.Rating) : 0,
                    Commands = AppRating.Select(review => review.Comment).ToList(),
                    CategoryId = response.Category.CategoryId,
                    CategoryName = response.Category.CategoryName,
                    Downloads = Count,
                    Status = response.Status

                });
                return appinfoDetails;
            }
            throw new EntityNotFoundException($"No AppInfo found for Id");
        }
    }
}