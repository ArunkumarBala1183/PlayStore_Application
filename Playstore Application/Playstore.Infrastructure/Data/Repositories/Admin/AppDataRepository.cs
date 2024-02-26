using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppData;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppDataRepository : IAppDataRepository
    {
        private readonly DatabaseContext database;

        private readonly IMapper mapper;

        public AppDataRepository(DatabaseContext context, IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
        }

        public async Task<object> GetAppData(Guid appId)
        {
            var appDetails = await this.database.AppDatas.FirstOrDefaultAsync(id => id.AppId == appId);

            if (appDetails != null)
            {
                return this.mapper.Map<RequestedAppDataDto>(appDetails);
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<object> UploadApp(IFormFile appFile, Guid appId)
        {
            if (appFile != null && appId != Guid.Empty)
            {
                var appData = new AppData();

                using (var stream = new MemoryStream())
                {
                    await appFile.CopyToAsync(stream);
                    appData.AppFile = stream.ToArray();
                    stream.Close();
                }

                appData.AppId = appId;
                appData.ContentType = appFile.ContentType;

                await this.database.AppDatas.AddAsync(appData);

                await this.database.SaveChangesAsync();

                return HttpStatusCode.Created;
            }

            return HttpStatusCode.NoContent;

        }
    }
}