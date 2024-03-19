using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO.AppReview;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using SqlException = Playstore.Core.Exceptions.SqlException;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppReviewRepository : Repository<AppReview>, IAppReviewRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger logger;
        public AppReviewRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            this.databaseContext=context;
            logger = Log.ForContext(Dataconstant.UserId, httpContext.HttpContext?.Items[Dataconstant.UserId])
                        .ForContext(Dataconstant.Location, typeof(AppReviewRepository).Name);
        }

        public async Task<object> GetReview(Guid id)
        {
            try{
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
                logger.Information(Dataconstant.AppReviewFetchedInfo+Dataconstant.Singlespace+id);
                return userReview;
            }
            
            logger.Information(Dataconstant.NoAppReviewFetchedInfo+Dataconstant.Singlespace+id);
            return HttpStatusCode.NoContent;
            }
            catch(SqlException exception)
            {
                throw new SqlException($"{exception}");
            }
            catch(Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }
    }
}