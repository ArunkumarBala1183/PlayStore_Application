using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Migrations.Scaffold;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class ApplicationLogsRepository : IApplicationLogsRepository
    {
        private readonly LogDbContext database;

        public ApplicationLogsRepository(LogDbContext context)
        {
            database = context;
        }
        public async Task<object> ViewLogs()
        {
            try
            {
                var logDetails = await this.database.AppLogs.ToListAsync();
    
                if(logDetails.Any())
                {
                    return logDetails;
                }
    
                return HttpStatusCode.NotFound;
            }
            catch (Exception error)
            {
                Log.Error(error , "Error : " + error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}