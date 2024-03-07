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
                 databaseContext=context;
            }
        // }
        // public async Task<object> GetAppDetails(Guid id)

        // {
    
        //     var response=await this.databaseContext.AppInfo.Include(data=>data.AppReview).Include(data=>data.AppDownloads).Include(data=>data.Category)
        //     .Where(obj=>obj.UserId==id)
        //     .ToListAsync();
        //     // var AppRating = await this.databaseContext.AppReviews.Where(obj => obj.AppId == response.AppId).ToListAsync();
        //     int Count=response.Count();
        //     if(response.Any())
        //     {
        //         var myappDetails=response.Select(response=>new Myappdetails
        //         {

        //             return new Myappdetails{
        //             AppId=response.AppId,
        //             Name=response.Name,
        //             Description=response.Description,
        //             Logo=response.Logo,
        //             PublishedDate=response.PublishedDate,
        //             PublisherName=response.PublisherName,
        //             Rating=response.AppReview.Any()? response.AppReview.Average(review=>review.Rating):0,
        //             CategoryId=response.Category.CategoryName,
        //             Downloads=Count
        //             };
        //         });
        //         return myappDetails;
        //     }
        //     throw new EntityNotFoundException($"No AppInfo found");
        // }


        public async Task<object> GetAppDetails(Guid userId)
{
    var response = await this.databaseContext.AppInfo
        .Include(data => data.AppDownloads)
        .Include(data => data.Category)
        .Where(obj => obj.UserId == userId)
        // .Where(status => status.Status == RequestStatus.Pending || status.Status == RequestStatus.Approved || status.Status == RequestStatus.Declined)
        .ToListAsync();
     int Count=response.Count();
    if (response.Any())
    {
        var myappDetails = response.Select(appInfo =>
        {
            var appReview = this.databaseContext.AppReviews
                .Where(review => review.AppId == appInfo.AppId)
                .ToList();
             var AppDownload=this.databaseContext.AppDownloads.Where(download=>download.AppId==appInfo.AppId).ToList();  
            
            return new Myappdetails
            {
                AppId = appInfo.AppId,
                Name = appInfo.Name,
                Description = appInfo.Description,
                Logo = appInfo.Logo,
                PublishedDate = appInfo.PublishedDate,
                PublisherName = appInfo.PublisherName,
                Apps=Count,
                Rating = appReview.Any() ? appReview.Average(review => review.Rating) : 0,
                CategoryName = appInfo.Category.CategoryName,
                Downloads = AppDownload.Count(),
                Status = appInfo.Status
            };
        }).ToList();

        return myappDetails;
    }

    throw new EntityNotFoundException($"No AppInfo found for UserId {userId}");
}
}
}   