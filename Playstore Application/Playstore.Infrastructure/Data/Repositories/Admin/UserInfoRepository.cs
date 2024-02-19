using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly DatabaseContext database;
        public UserInfoRepository(DatabaseContext context)
        {
            this.database = context;
        }
        public async Task<object> ViewAllUsers()
        {
            var existedData = await this.database.Users.ToListAsync();

            if(existedData != null && existedData.Count > 0)
            {
                return existedData;
            }

            return HttpStatusCode.NoContent;
        }
    }
}