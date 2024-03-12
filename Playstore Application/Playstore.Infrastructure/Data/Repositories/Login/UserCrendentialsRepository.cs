using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;
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
        public new async Task<bool> Update(UserCredentials userCredentials)
        {

            _context.UserCredentials.Update(userCredentials);
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<UserCredentials> GetByEmailAsync(string email)
        {
            var user = await _context.UserCredentials.FirstOrDefaultAsync(x => x.EmailId == email);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with email {email} not found.");

            }
            return user;
        }

        public async Task<UserCredentials> GetByIdAsync(Guid userId)
        {
            var id = await _context.UserCredentials.FirstOrDefaultAsync(u => u.UserId == userId);
            if (id == null)
            {
                throw new Exception("Id is null");
            }
            return id;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<string> ChangePassword(Guid userId, string hashedPassword)
        {
            var user = await _context.UserCredentials.Where(user => user.UserId == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Password = hashedPassword;
                _context.UserCredentials.Update(user);
                await _context.SaveChangesAsync();
                return "Password Changed Successfully";
            }
            else
            {
                return "User not found";
            }
        }
    }
}