using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly DatabaseContext _context;
        public UsersRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        public async Task<object> GetAll(Guid id)
        {
            var response = await _context.Users.FindAsync(id);

            if (response != null)
            {
                return response;
            }

            return HttpStatusCode.NoContent;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Users> GetByEmailId(string emailId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailId == emailId);

            if (user == null)
            {
                throw new EntityNotFoundException($"User with email {emailId} not found.");
            }

            return user;
        }

        public async Task<Users> GetByPhoneNumber(string mobileNumber)
        {
            var number = await _context.Users.FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber);
            if (number == null)
            {
                throw new EntityNotFoundException($"User with mobile number {mobileNumber} not found.");
            }

            return number;
        }
    }
}
