using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO.AppReview;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppReviewRepository : Repository<AppReview>, IAppReviewRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger logger;
        public AppReviewRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            this.databaseContext=context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(AppReviewRepository).Name);
        }

        public async Task<object> GetReview(Guid id)
        {
            var response = await this.databaseContext.AppReviews.Include(obj => obj.Users).Where(obj => obj.AppId == id).ToListAsync();
            if(response!=null)
            {
                var userReview = response.Select(obj => 
                {
                    return new AppReviewDetailsDTO
                    {
                        AppId = obj.AppId,
                        Username = response.Select(obj => obj.Users.Name).ToList(),
                        Commands = response.Select(obj => obj.Comment).ToList(),
                        Ratings = response.Select(obj => obj.Rating).ToList()
                    };
                });
                logger.Information($"AppReview fetched for id {id}");
                return userReview;
            }
            
            logger.Information($"No AppReview found for id {id}");
            return HttpStatusCode.NoContent;
        }
    }
}