using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories
{
    public class UserCredentialsRepository : Repository<UserCredentials>, IUserCredentialsRepository
    {
        private readonly DatabaseContext _context;
        private readonly ILogger logger;
        public UserCredentialsRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            _context = context;
            logger = Log.ForContext("Location", typeof(UserCredentialsRepository).Name);
        }
        public async Task<bool> UpdateCredentials(UserCredentials userCredentials)
        {
            _context.UserCredentials.Update(userCredentials);
            await _context.SaveChangesAsync();

            logger.Information("UserCredentials Updated");
            return true;

        }
        public async Task<UserCredentials?> GetByEmailId(string email)
        {
            var response =  await _context.UserCredentials.FirstOrDefaultAsync(mailid => mailid.EmailId == email);
            logger.Information($"UserCredetial Fetched for the email {email}");
            return response;
            
        }

        public async Task<UserCredentials?> GetById(Guid userId)
        {
            var response = await _context.UserCredentials.FirstOrDefaultAsync(id => id.UserId == userId);
            logger.Information($"UserCredentials fetched for Id {userId}");
            return response;

        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangePassword(Guid UserId, string Password)
        {
            var user=await _context.UserCredentials.Where(user=>user.UserId==UserId).FirstOrDefaultAsync();
            if(user != null)
            {
                user.Password=Password;
                _context.UserCredentials.Update(user);
                await _context.SaveChangesAsync();
                logger.Information($"Password Changed for userid {userId}");
                return true;
                
            }
            logger.Information($"No User found for userid {userId}");
            return false;
        }

        public async Task<bool> checkPassword(Guid UserId , string hashedPassword)
        {
             bool isPasswordExist = _context.UserCredentials.Any(userId => userId.UserId == UserId&& userId.Password==hashedPassword);
             return isPasswordExist;
        }
    }
}