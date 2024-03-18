using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.TotalDownloads;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppDownloadsRepository : IAppDownloadsRepository
    {
        private readonly DatabaseContext database;
        private readonly ILogger logger;
        public AppDownloadsRepository(DatabaseContext context, IMapper mapper , IHttpContextAccessor httpContext)
        {
            this.database = context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(AppDownloadsRepository).Name);
        }


        public async Task<object> GetAppLogs(AppLogsDto appSearch)
        {
            try
            {
                if (appSearch.FromDate is null && appSearch.DownloadedDate is null && appSearch.AppName is null && appSearch.UserName is null)
                {
                    logger.Information("Not a Valid Input");
                    return HttpStatusCode.BadRequest;
                }
    
                var query = this.database.AppDownloads
                .Include(app => app.AppInfo)
                .Include(user => user.Users)
                .AsQueryable();

                var appLogs = await this.CheckValues(appSearch , query);
    
                if (appLogs != null && appLogs.Count > 0)
                {
                    logger.Information("AppDownloads Records Fetched from Server");
                    return appLogs;
                }
                logger.Information("No AppDownloads Records Found");
                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }


        public async Task<object> GetTotalDownloadsByDate()
        {
            try
            {
                DateTime todayDate = DateTime.Now.Date;
                DateTime lastWeekDate = todayDate.AddDays(-6);

                List<int> appCount = new List<int>();
                List<string> dates = new List<string>();

                for(DateTime date = lastWeekDate; date <= todayDate; date = date.AddDays(1))
                {
                    int count = await database.AppDownloads
                        .Where(d => d.DownloadedDate.Date == date)
                        .CountAsync();

                    appCount.Add(count);
                    dates.Add(date.ToString("yyyy-MM-dd"));
                }

                logger.Information("AppDownloads Records Fetched from Server");
                return new DownloadDetailsDto{
                    count = appCount,
                    Dates = dates
                };
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }



        private async Task<List<AppDownloadsDto>> CheckValues(AppLogsDto appSearch , IQueryable<AppDownloads> query)
        {   


            if (appSearch.UserName is not null)
            {
                query = query.Where(user => user.Users.Name.Contains(appSearch.UserName));
            }

            if (appSearch.AppName is not null)
            {
                query = query.Where(app => app.AppInfo.Name.Contains(appSearch.AppName));
            }

            if (appSearch.DownloadedDate.HasValue && appSearch.FromDate.HasValue)
            {
                query = query.Where(date => date.DownloadedDate.Date >= appSearch.FromDate.Value && date.DownloadedDate.Date <= appSearch.DownloadedDate.Value);
            }
            else if(appSearch.FromDate.HasValue)
            {
                query = query.Where(date => date.DownloadedDate.Date >= appSearch.FromDate.Value);
            }
            else if(appSearch.DownloadedDate.HasValue)
            {
                query = query.Where(date => date.DownloadedDate.Date <= appSearch.DownloadedDate.Value);
            }

            var appLogs = await query.Select(values => new AppDownloadsDto{
                    DownloadedDate = values.DownloadedDate.ToString("yyyy-MM-dd"),
                    appName = values.AppInfo.Name,
                    userName = values.Users.Name
                })
                .ToListAsync();

            return appLogs;
        }
    }
}