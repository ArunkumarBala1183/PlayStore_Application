using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppRepository : Repository<App>, IAppRepository
    {
        public AppRepository(DatabaseContext context) : base(context)
        {
        }

        public void AppDetails()
        {
            Console.WriteLine("App Details");
        }
    }
}