using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories
{
    public class GetUsersDetailsRepository : Repository<UsersDetailsDTO>, IUserDetailsRepository
    {
        private readonly DatabaseContext databaseContext;
        public GetUsersDetailsRepository(DatabaseContext context) : base(context)
        {
            databaseContext=context;
        }
        public async Task<object> GetUsersDetails(Guid userId)
        {
            var userDetails=await databaseContext.Users
            .Include(data=>data.UserRoles)
            .ThenInclude(role => role.Role)
            .Where(Id=>Id.UserId==userId)
            .FirstOrDefaultAsync();

            if(userDetails != null)
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

                return UserData;
            }

            return HttpStatusCode.NotFound;
        }

       
    }
}