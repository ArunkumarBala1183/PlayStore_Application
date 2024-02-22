using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppDownloadsRepository : IAppDownloadsRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        public AppDownloadsRepository(DatabaseContext context , IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
        }
        public async Task<object> GetAppLogs(AppLogsDto appSearch)
        {
            var appDownloads = await this.database.AppDownloads
            .Where(date => date.DownloadedDate.Date == DateTime.UtcNow.Date)
            .ToListAsync();
            
            if(appDownloads != null && appDownloads.Count > 0)
            {
                var appDownloadsDto = this.mapper.Map<IEnumerable<AppDownloadsDto>>(appDownloads);
                return appDownloadsDto;
            }

            return HttpStatusCode.NoContent;
        }
    }
}