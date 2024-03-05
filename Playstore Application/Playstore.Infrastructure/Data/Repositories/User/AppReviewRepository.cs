using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO.AppReview;

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
            var response = await this.databaseContext.AppReviews.Include(obj => obj.Users).Where(obj => obj.AppId == id).ToListAsync();
            if(response!=null)
            {
                var userReview = response.Select(obj => 
                {
                    var users = this.databaseContext.Users.Where(Userobj => Userobj.UserId == obj.UserId).ToList();
                    return new AppReviewDetailsDTO
                    {
                        AppId = obj.AppId,
                        Username = response.Select(obj => obj.Users.Name).ToList(),
                        // Commands = response.GroupBy(obj => obj.UserId).ToDictionary(obj => obj.Key, obj => obj.Select(obj => obj.Comment).ToList())
                        Commands = response.Select(obj => obj.Comment).ToList()
                    };
                });
                return userReview;
            }

            return HttpStatusCode.NoContent;
        }
    }
}