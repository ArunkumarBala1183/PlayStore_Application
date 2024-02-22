using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        public UserInfoRepository(DatabaseContext context , IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
        }
        public async Task<object> ViewAllUsers()
        {
            var existedData = await this.database.Users.Include(userrole => userrole.UserRoles).ToListAsync();

            if(existedData != null && existedData.Count > 0)
            {
                var userDetails = this.mapper.Map<IEnumerable<UserInfoDto>>(existedData);
                return userDetails;
            }

            return HttpStatusCode.NoContent;
        }
    }
}