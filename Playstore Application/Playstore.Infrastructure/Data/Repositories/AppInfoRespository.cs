using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class AppInfoRespository : IAppInfoRepository
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;

        public AppInfoRespository(DatabaseContext database , IMapper mapper)
        {
            this._database = database;
            this._mapper = mapper;
        }

        public async Task<HttpStatusCode> RemoveApp(Guid id)
        {
            var existedData = await this._database.AppInfo.FindAsync(id);

            if(existedData != null)
            {
                this._database.AppInfo.Remove(existedData);

                await this._database.SaveChangesAsync();
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<object> ViewAllApps()
        {
            var appDetails = await this._database.AppInfo.ToListAsync();
            
            if(appDetails != null && appDetails.Count > 0)
            {
                return this._mapper.Map<IEnumerable<AppInfoDto>>(appDetails);
            }
            else
            {
                return HttpStatusCode.NoContent;
            }
        }
    }
}