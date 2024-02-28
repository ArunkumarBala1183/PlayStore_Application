using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppFilesRepository : Repository<AppInfo>, IAppFilesRepository
    {
        private readonly DatabaseContext databaseContext;

        public AppFilesRepository(DatabaseContext context) : base(context)
        {
            databaseContext = context;
        }


        public async Task<HttpStatusCode> AddFiles(CreateAppInfoDTO createAppInfoDTO)
        {
            try
            {
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

                Console.WriteLine("Saved App Data");

                var user = await databaseContext.Users
                .Include(data => data.UserRoles)
                .FirstOrDefaultAsync(id => id.UserId == createAppInfoDTO.UserId);

                if (user != null)
                {
                    Console.WriteLine(user.UserRoles.Count);
                    if (user.UserRoles.Count > 1)
                    {
                        appInfo.Status = RequestStatus.Approved;
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
            catch (Exception error)
            {
                Console.WriteLine(error.Message);

                return HttpStatusCode.InternalServerError;
            }
        }


    }


}