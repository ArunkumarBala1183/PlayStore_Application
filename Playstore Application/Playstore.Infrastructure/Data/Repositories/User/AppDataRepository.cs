using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppDataRepository : Repository<AppData>, IAppDataRepository
    {
        public AppDataRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}