using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppRequestsRepository : IAppRequestsRepository
    {
        private readonly DatabaseContext database;
        public AppRequestsRepository(DatabaseContext context)
        {
            this.database = context;
        }
        public async Task<object> GetAllRequests()
        {
            var requests = await this.database.AdminRequests
            .Include(userInfo => userInfo.Users)
            .Include(appInfo => appInfo.AppInfo)
            .Where(status => status.AppInfo.Status == RequestStatus.Pending)
            .ToListAsync();

            if(requests != null && requests.Count > 0)
            {
                return requests;
            }

            return HttpStatusCode.NoContent;
        }
    }
}