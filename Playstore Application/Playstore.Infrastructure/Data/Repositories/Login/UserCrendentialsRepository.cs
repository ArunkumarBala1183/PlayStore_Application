using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public async Task<bool> Update(UserCredentials userCredentials)
        {
            
                _context.UserCredentials.Update(userCredentials);
                await _context.SaveChangesAsync();

                return true; 
            
        }
        // public async Task<UserCredentials> GetByConditionAsync(Expression<Func<UserCredentials, bool>> condition)
        // {
        //     return await _context.UserCredentials.Where(condition).FirstOrDefaultAsync();
        // }
        public async Task<UserCredentials> GetByEmailAsync(string email)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(x => x.EmailId == email);
        }

        public async Task<UserCredentials> GetByIdAsync(Guid userId)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        
        //public IUsersRepository Users => new UsersRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        // public async Task<UserCredentials> GetByEmailWithRolesAsync(Guid id)
        // {
        //     return await _context.UserCredentials
        //         .Include(uc => uc.User)  // Include the User entity if needed
        //         .Include(uc => uc.User.UserRoles)
        //             .ThenInclude(ur => ur.Role)
        //         .FirstOrDefaultAsync(x => x.UserId == id);
        // }
        public async Task<bool> ChangePassword(Guid userId, string password)
        {
            var user=await _context.UserCredentials.Where(user=>user.UserId==userId).FirstOrDefaultAsync();
            if(user != null)
            {
            Console.WriteLine("++++++++++++++++++password Not FOund+++++++++++++++++++");
                user.Password=password;
                _context.UserCredentials.Update(user);
                _context.SaveChangesAsync();
                 return true;
            }
            return false;
        }

        public async Task<bool> checkPassword(Guid UserId , string hashedPassword)
        {
             bool isPasswordExist = _context.UserCredentials.Any(userId => userId.UserId == UserId&& userId.Password==hashedPassword);
             return isPasswordExist;
           
        }
    }
}