using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
            try
            {
                var appDetails = await this.database.AppDatas.FirstOrDefaultAsync(id => id.AppId == appId);
    
                if (appDetails != null)
                {
                    return this.mapper.Map<RequestedAppDataDto>(appDetails);
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

        public async Task<HttpStatusCode> UploadApp(IFormFile appFile, Guid appId)
        {
            try
            {
                if (appFile != null && appId != Guid.Empty)
                {
                    var appDetails = await this.database.AppInfo.FindAsync(appId);
                    var appData = new AppData();
    
                    using (var stream = new MemoryStream())
                    {
                        await appFile.CopyToAsync(stream);
                        appData.AppFile = stream.ToArray();
                        appDetails.Logo = stream.ToArray();
                        stream.Close();
                    }
    
                    appData.AppId = appId;
                    appData.ContentType = appFile.ContentType;
    
                    await this.database.AppDatas.AddAsync(appData);

                    this.database.AppInfo.Update(appDetails);
    
                    await this.database.SaveChangesAsync();
    
                    return HttpStatusCode.Created;
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
    }
}