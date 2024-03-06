using System.Net;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        public UserInfoRepository(DatabaseContext context, IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
        }

        public async Task<object> SearchUserDetail(string searchDetails)
        {
            try
            {
                var userDetails = new List<Users>();
                var query = database.Users
                .Include(userRole => userRole.UserRoles)
                .ThenInclude(role => role.Role)
                .AsQueryable();

                if (!string.IsNullOrEmpty(searchDetails))
                {
                    userDetails = await query.Where(user =>
                    user.Name.Contains(searchDetails) ||
                    user.EmailId.Contains(searchDetails) ||
                    user.MobileNumber.Contains(searchDetails) ||
                    user.UserRoles.Any(role => role.Role.RoleCode.Contains(searchDetails)))
                    .ToListAsync();
                    
                }
                else
                {
                    userDetails = await query.ToListAsync();
                }

                if (userDetails != null && userDetails.Count > 0)
                {
                    return mapper.Map<IEnumerable<UserInfoDto>>(userDetails);
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
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

        public async Task<object> ViewAllUsers()
        {
            try
            {
                var existedData = await this.database.Users.
                Include(userrole => userrole.UserRoles)
                .ThenInclude(role => role.Role)
                .ToListAsync();

                if (existedData != null && existedData.Count > 0)
                {
                    var userDetails = this.mapper.Map<IEnumerable<UserInfoDto>>(existedData);
                    return userDetails;
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