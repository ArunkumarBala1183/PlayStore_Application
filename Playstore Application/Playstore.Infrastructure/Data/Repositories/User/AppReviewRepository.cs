using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppReviewRepository : Repository<AppReview>, IAppReviewRepository
    {
        private readonly DatabaseContext databaseContext;
        public AppReviewRepository(DatabaseContext context) : base(context)
        {
            this.databaseContext = context;
        }
        //Get Review Details For AppReview Tables
        public async Task<object> GetReview(Guid AppId)
        {
            var response = await this.databaseContext.AppReviews.Include(obj => obj.Users).Where(obj => obj.AppId == AppId).ToListAsync();
            if (response.Any())
            {
                var userReview = response.Select(obj =>
                {
                    var reviewAppId = this.databaseContext.Users.Where(review => review.UserId == obj.UserId).ToList();
                    return new AppReviewDetailsDTO
                    {
                        AppId = obj.AppId,
                        Username = response.Select(username => username.Users.Name).ToList(),
                        Commands = response.GroupBy(item => item.UserId).ToDictionary(group => group.Key, group => group.Select(item => item.Comment).ToList())


                    };
                });
                return userReview;
            }
           throw new EntityNotFoundException($"No Apps found");
        }
    }
}