using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class UserCredentialsRepository : Repository<UserCredentials>, IUserCredentialsRepository
    {
        private readonly DatabaseContext _context;
        public UserCredentialsRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> UpdateCredentials(UserCredentials userCredentials)
        {
            _context.UserCredentials.Update(userCredentials);
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<UserCredentials?> GetByEmailId(string email)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(mailid => mailid.EmailId == email);
            
        }

        public async Task<UserCredentials?> GetById(Guid userId)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(id => id.UserId == userId);

        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangePassword(Guid userId, string password)
        {
            var user=await _context.UserCredentials.Where(user=>user.UserId==userId).FirstOrDefaultAsync();
            if(user != null)
            {
                user.Password=password;
                _context.UserCredentials.Update(user);
                await _context.SaveChangesAsync();
                 return true;
            }
            return false;
        }
    }
}