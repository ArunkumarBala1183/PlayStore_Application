using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly DatabaseContext _context;
        private readonly ILogger logger;
        public UsersRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            _context = context;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(UserRepository).Name);
        }
        public async Task<object> GetAll(Guid id)
        {
            var response = await _context.Users.FindAsync(id);

            if(response != null)
            {
                logger.Information("User fetched from server");
                return response;
            }
            logger.Information("No User found");
            return HttpStatusCode.NoContent;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Users?> GetByEmailId(string emailId)
        {
            var response =  await _context.Users.FirstOrDefaultAsync(mailid => mailid.EmailId == emailId);
            logger.Information($"User fetched for {emailId}");
            return response;
        }

        public async Task<Users?> GetByPhoneNumber(string mobileNumber)
        {
            var response =  await _context.Users.FirstOrDefaultAsync(number => number.MobileNumber == mobileNumber);
            logger.Information("User fetched from server");
            return response;
            
        }
        
    }
}
