using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class AppAdminRequestRepository : Repository<AdminRequests>, IAppAdminRequestRepository
    {
        public readonly DatabaseContext databaseContext;
        public AppAdminRequestRepository(DatabaseContext context) : base(context)
        {
            this.databaseContext=context;
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
             return Guidvalue;
            }
            return HttpStatusCode.NoContent;
        }

    }
}