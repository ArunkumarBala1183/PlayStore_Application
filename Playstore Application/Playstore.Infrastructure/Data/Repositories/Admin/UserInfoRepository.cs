using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        private readonly RoleConfig role;
        private readonly ILogger logger;
        public UserInfoRepository(DatabaseContext context, IMapper mapper , IOptions<RoleConfig> options , IHttpContextAccessor httpContext)
        {
            this.database = context;
            this.mapper = mapper;
            this.role = options.Value;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(UserInfoRepository).Name);
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
                    (user.Name.Contains(searchDetails) ||
                    user.EmailId.Contains(searchDetails) ||
                    user.MobileNumber.Contains(searchDetails) ||
                    user.UserRoles.Any(role => role.Role.RoleCode.Contains(searchDetails))) &&
                    user.UserRoles.Any(roles => roles.Role.RoleCode != role.AdminCode))
                    .ToListAsync();
                    
                }
                else
                {
                    userDetails = await query
                    .Where(user => user.UserRoles.Any(roles => roles.Role.RoleCode != role.AdminCode)) 
                    .ToListAsync();
                }

                if (userDetails != null && userDetails.Count > 0)
                {
                    logger.Information("UserDetails Fetched from Database");
                    return mapper.Map<IEnumerable<UserInfoDto>>(userDetails);
                }
                else
                {
                    logger.Information("No Users Found");
                    return HttpStatusCode.NotFound;
                }
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

        public async Task<object> ViewAllUsers()
        {
            try
            {
                var existedData = await this.database.Users
                .Include(userRole => userRole.UserRoles)
                .ThenInclude(role => role.Role)
                .Where(userRole => userRole.UserRoles.Any(roles => roles.Role.RoleCode == role.UserCode || roles.Role.RoleCode == role.DeveloperCode))
                .ToListAsync();

                if (existedData != null && existedData.Count > 0)
                {
                    var userDetails = this.mapper.Map<IEnumerable<UserInfoDto>>(existedData);
                    logger.Information("UserDetails Fetched from Database");

                    return userDetails;
                }
                logger.Information("No Users Found");

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