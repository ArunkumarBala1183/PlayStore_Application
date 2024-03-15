using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO.AppReview;
using Microsoft.Data.SqlClient;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppReviewRepository : Repository<AppReview>, IAppReviewRepository
    {
        private readonly DatabaseContext databaseContext;
        public AppReviewRepository(DatabaseContext context) : base(context)
        {
            this.databaseContext=context;
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
                return userReview;
            }

            return HttpStatusCode.NoContent;
            }
            catch(SqlException exception)
            {
                throw new Exception($"{exception}");
            }
            catch(Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }
    }
}