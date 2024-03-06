using System.Net;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.TotalDownloads;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppDownloadsRepository : IAppDownloadsRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        public AppDownloadsRepository(DatabaseContext context, IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
        }


        public async Task<object> GetAppLogs(AppLogsDto appSearch)
        {
            try
            {
                if (appSearch.DownloadedDate is null && appSearch.AppId is null && appSearch.UserId is null)
                {
                    return HttpStatusCode.BadRequest;
                }

                var appLogs = await this.CheckValues(appSearch)
                .Include(app => app.AppInfo)
                .ThenInclude(category => category.Category)
                .Include(user => user.Users)
                .ThenInclude(roleUser => roleUser.UserRoles)
                .ThenInclude(role => role.Role)
                .ToListAsync();

                if (appLogs != null && appLogs.Count > 0)
                {
                    return this.mapper.Map<IEnumerable<AppDownloadsDto>>(appLogs);
                }

                return HttpStatusCode.NotFound;
            }
            catch (SqlException)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }





        // public async Task<IEnumerable<int>> GetTotalDownloads()
        // {
        //      DateTime TodayDate = DateTime.Now.Date;
        //      DateTime LastWeekDate = TodayDate.AddDays(-7);

        //      var Dates = await this.database.AppDownloads.Where(d=> d.DownloadedDate >= LastWeekDate && d.DownloadedDate <=TodayDate).ToListAsync();
        //      foreach (var date in Dates)
        //      {
        //         var downloads=await this.database.AppDownloads.Where(download=>download.DownloadedDate==date).Count();
        //         return downloads;

        //      }
        //      return (IEnumerable<int>)TotalDownloads;
        // }


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


                return new DownloadDetailsDto{
                    count = appCount,
                    Dates = dates
                };
            }
            catch (SqlException)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }



        private IQueryable<AppDownloads> CheckValues(AppLogsDto appSearch)
        {
            var query = this.database.AppDownloads.AsQueryable();

            if (appSearch.UserId.HasValue)
            {
                Console.WriteLine("UserId has value");
                query = query.Where(user => user.UserId == appSearch.UserId);
            }

            if (appSearch.AppId.HasValue)
            {
                Console.WriteLine("AppId has value");
                query = query.Where(app => app.AppId == appSearch.AppId);
            }

            if (appSearch.DownloadedDate.HasValue)
            {
                Console.WriteLine("Date has value");
                query = query.Where(date => date.DownloadedDate.Date == appSearch.DownloadedDate.Value);
            }

            return query;
        }
    }
}