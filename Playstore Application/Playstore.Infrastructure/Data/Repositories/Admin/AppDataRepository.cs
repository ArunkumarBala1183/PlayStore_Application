using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppData;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppDataRepository : IAppDataRepository
    {
        private readonly DatabaseContext database;

        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AppDataRepository(DatabaseContext context, IMapper mapper , IHttpContextAccessor httpContext)
        {
            this.database = context;
            this.mapper = mapper;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(AppDataRepository).Name);
        }

        public async Task<object> GetAppData(Guid appId)
        {
            try
            {
                var appDetails = await this.database.AppDatas.FirstOrDefaultAsync(id => id.AppId == appId);
    
                if (appDetails != null)
                {
                    logger.Information($"{appId} Fetched from Server");
                    return this.mapper.Map<RequestedAppDataDto>(appDetails);
                }

                logger.Information($"{appId} Not Found in Server");
                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error ,error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> UploadApp(IFormFile appFile, Guid appId)
        {
            try
            {
                if (appFile != null && appId != Guid.Empty)
                {
                    if (Path.GetExtension(appFile.FileName).Equals(".zip"))
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
                    }
                    else
                    {
                        var appDetails = await this.database.AppInfo.FindAsync(appId);

                        if (appDetails != null)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await appFile.CopyToAsync(stream);
                                appDetails.Logo = stream.ToArray();
                                stream.Close();
                            }

                            this.database.Update(appDetails);
                        }
                    }
    
                    await this.database.SaveChangesAsync();

                    logger.Information($"{appFile.FileName} has Uploaded in Server");
    
                    return HttpStatusCode.Created;
                }
                logger.Information($"{appId} Not Found in Server");
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
    }
}