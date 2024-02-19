using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppDownloadsRepository : IAppDownloadsRepository
    {
        private readonly DatabaseContext database;
        public AppDownloadsRepository(DatabaseContext context)
        {
            this.database = context;
        }
        public async Task<object> GetAllAppDownloads()
        {
            var appDownloads = await this.database.AppDownloads.ToListAsync();
            
            if(appDownloads != null && appDownloads.Count > 0)
            {
                return appDownloads;
            }

            return HttpStatusCode.NoContent;
        }
    }
}