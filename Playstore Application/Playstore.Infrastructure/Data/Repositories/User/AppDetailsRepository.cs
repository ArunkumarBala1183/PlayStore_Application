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
    public class AppDetailsRepository : Repository<AppInfo>, IAppDetailsRepository
    {
        public readonly DatabaseContext databaseContext;
        private readonly ILogger logger;
        public AppDetailsRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            this.databaseContext=context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(AppDetailsRepository).Name);
        }

        public async Task<object> GetAppDetails(Guid id,Guid userId)
        {
            var response = await this.databaseContext.AppInfo.Include(obj=>obj.Category).Include(obj=>obj.AppImages).Where(obj=>obj.AppId==id).ToListAsync();
            var appImages = await this.databaseContext.AppImages.Where(obj => obj.AppId== id).ToListAsync();
            var AppRating=await this.databaseContext.AppReviews.Where(obj=>obj.AppId==id).ToListAsync();
            var Value=await this.databaseContext.AppDownloads.Where(obj=>obj.AppId==id).ToListAsync();
            var AppDownloadCount=await databaseContext.AppDownloads.Where(obj=>obj.UserId==userId&&obj.AppId==id).ToListAsync();
            int Count=Value.Count;
            int Totalvalue=response.Count;
            int ParticularAppDownloadCount=AppDownloadCount.Count;
            

            if(response.Any())
            {
                var appinfoDetails=response.Select(response=> new AppInfoDetailsDTO
                {
                    appImages=appImages.Select(image=>image.Image).ToList(),
                    AppId=response.AppId,
                    Name=response.Name,
                    Description=response.Description,
                    Logo=response.Logo,
                    UserId=response.UserId,
                    Apps=Totalvalue,
                    Rating=AppRating.Any()? AppRating.Average(review=>review.Rating):0,
                    Commands = AppRating.Select(review => review.Comment).ToList(),
                    CategoryId=response.Category.CategoryId,
                    CategoryName=response.Category.CategoryName,
                    Downloads=Count,
                    ParticularUserDownloadCount = ParticularAppDownloadCount,
                    PublisherName = response.PublisherName,
                    PublishedDate = response.PublishedDate                    
                });
                logger.Information($"AppDetails fetched for id {id}");
                return appinfoDetails;
            }

            var message = $"No AppInfo found for Id {id}";
            logger.Information(message);
            throw new EntityNotFoundException(message); 
            
            
        }
    }
}