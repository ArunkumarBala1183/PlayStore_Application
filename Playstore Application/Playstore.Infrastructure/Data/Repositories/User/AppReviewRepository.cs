using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            var response = await this.databaseContext.AppReviews.FirstOrDefaultAsync(appId => appId.AppId == id);
            if(response!=null)
            {
                return response;
            }
            return HttpStatusCode.NoContent;
        }
    }
}