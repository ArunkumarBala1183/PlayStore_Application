using System.Net;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;
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