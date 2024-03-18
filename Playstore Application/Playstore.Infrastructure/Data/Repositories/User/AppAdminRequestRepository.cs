using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppAdminRequestRepository : Repository<AdminRequests>, IAppAdminRequestRepository
    {
        public readonly DatabaseContext databaseContext;
        private readonly ILogger logger;
        public AppAdminRequestRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            this.databaseContext=context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(AppAdminRequestRepository).Name);
        }

        public async Task<object> AddRequest(Guid id)
        {
            var request=this.databaseContext.Users.FirstOrDefault(x=>x.UserId==id);
            if(request!=null)
            {
                var Adminvalue=new AdminRequests
                {
                    UserId=id,
                };
                databaseContext.AdminRequests.Add(Adminvalue);
                databaseContext.SaveChanges();
                
                var Guidvalue=await databaseContext.AdminRequests.Where(x => x.UserId == id).ToListAsync();
                logger.Information("Requested Created for the App Upload");
             return Guidvalue;
            }
            logger.Information($"No User found for id {id}");
            return HttpStatusCode.NoContent;
        }

    }
}