using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class GetCategoryRepository : Repository<Category>, IGetCategory
    {
        public GetCategoryRepository(DatabaseContext context) : base(context)
        {
        }

      
    }
}