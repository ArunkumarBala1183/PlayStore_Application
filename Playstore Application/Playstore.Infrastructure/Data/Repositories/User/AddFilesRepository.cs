using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Contracts.DTO;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppFilesRepository : Repository<AppInfo>, IAppFilesRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly RoleConfig role;

        public AppFilesRepository(DatabaseContext context , IOptions<RoleConfig> roleConfig) : base(context)
        {
            databaseContext = context;
            this.role = roleConfig.Value;
        }


        public async Task<HttpStatusCode> AddFiles(CreateAppInfoDTO createAppInfoDTO)
        {
            try
            {
                bool name = this.databaseContext.AppInfo.Any(appName => appName.Name == createAppInfoDTO.Name);
                if(!name){
                var newRequest = new AdminRequests()
                {
                    UserId = createAppInfoDTO.UserId
                };

                await this.databaseContext.AdminRequests.AddAsync(newRequest);

                var appInfo = new AppInfo
                {
                    Name = createAppInfoDTO.Name,
                    Description = createAppInfoDTO.Description,
                    PublisherName = createAppInfoDTO.PublisherName,
                    RequestId = newRequest.Id,
                
                    CategoryId = createAppInfoDTO.CategoryId,
                    UserId = createAppInfoDTO.UserId
                };

                using (var memoryStream = new MemoryStream())
                {
                    await createAppInfoDTO.Logo.CopyToAsync(memoryStream);

                    appInfo.Logo = memoryStream.ToArray();

                    memoryStream.Close();

                }

                var appData = new AppData
                {
                    ContentType = createAppInfoDTO.AppFile.ContentType,
                    AppId = appInfo.AppId
                };

                using (var MemoryStream = new MemoryStream())
                {
                    await createAppInfoDTO.AppFile.CopyToAsync(MemoryStream);
                    appData.AppFile = MemoryStream.ToArray();

                    MemoryStream.Close();
                }

                appInfo.AppData = appData;

                ICollection<AppImages> appImages = new List<AppImages>();

                foreach (var files in createAppInfoDTO.appImages)
                {
                    using (var MemoryImages = new MemoryStream())
                    {
                        await files.CopyToAsync(MemoryImages);
                        var Images = new AppImages
                        {
                            Image = MemoryImages.ToArray(),
                            AppId = appInfo.AppId
                        };
                        
                        appImages.Add(Images);

                    }


                }

                appInfo.AppImages = appImages;

                

                var user = await databaseContext.Users
                .Include(data => data.UserRoles)
                .FirstOrDefaultAsync(id => id.UserId == createAppInfoDTO.UserId);

                if (user != null)
                {
                    if (user.UserRoles.Where(id => id.RoleId == role.DeveloperId).Any())
                    {
                        appInfo.Status = RequestStatus.Approved;
                        appInfo.PublishedDate = DateTime.Today;
                    }
                    else
                    {
                        appInfo.Status = RequestStatus.Pending;
                    }

                    await this.databaseContext.AppInfo.AddAsync(appInfo);

                    await databaseContext.SaveChangesAsync();

                    return HttpStatusCode.Created;
                }

                return HttpStatusCode.NotFound;
                }
                return HttpStatusCode.BadRequest;
            }
            catch(SqlException exception)
            {
                throw new Exception($"{exception}");
            }
            catch (Exception error)
            {
               
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}