using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Migrations.Scaffold;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class ApplicationLogsRepository : IApplicationLogsRepository
    {
        private readonly LogDbContext database;
        private readonly ILogger logger;

        public ApplicationLogsRepository(LogDbContext context , IHttpContextAccessor httpContext)
        {
            database = context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(ApplicationLogsRepository).Name);
        }
        public async Task<object> ViewLogs()
        {
            try
            {
                var logDetails = await this.database.AppLogs.OrderByDescending(time => time.TimeStamp).ToListAsync();
    
                if(logDetails.Any())
                {
                    logger.Information("Application Logs Fetched from Server");
                    return logDetails;
                }
                
                logger.Information("No Application Logs found in Server");
                
                return HttpStatusCode.NotFound;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}