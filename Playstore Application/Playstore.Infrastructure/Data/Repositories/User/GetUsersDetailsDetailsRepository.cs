using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Core.Exceptions;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using SqlException = Playstore.Core.Exceptions.SqlException;
using SpecificException = Playstore.Core.Exceptions.SqlException;

using Serilog;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class GetUsersDetailsRepository : Repository<UsersDetailsDTO>, IUserDetailsRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger logger;
        public GetUsersDetailsRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            databaseContext=context;
            logger = Log.ForContext(Dataconstant.UserId, httpContext.HttpContext?.Items[Dataconstant.UserId])
                        .ForContext(Dataconstant.Location, typeof(GetUsersDetailsRepository).Name);
        }
        public async Task<object> GetUsersDetails(Guid userId)
        {
            try
            {
                var userDetails = await databaseContext.Users
                .Include(data => data.UserRoles)
                .ThenInclude(role => role.Role)
                .Where(Id => Id.UserId == userId)
                .FirstOrDefaultAsync();

                if (userDetails != null)
                {
                    List<string> roles = new List<string>();

                    foreach (var role in userDetails.UserRoles)
                    {
                        roles.Add(role.Role.RoleCode);
                    }

                var UserData = new UsersDetailsDTO
                {
                    Name = userDetails.Name,
                    Email = userDetails.EmailId,
                    PhoneNumber = userDetails.MobileNumber,
                    Role = roles
                };
                logger.Information(Dataconstant.UserdetailsFetched+Dataconstant.Singlespace+userId);
                return UserData;
            }
            logger.Information(Dataconstant.NoUserFoundForId+Dataconstant.Singlespace+userId);
            return HttpStatusCode.NotFound;
        }

       
            catch(SqlException exception)
            {
                throw new SqlException($"{exception}");
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }

    }
}