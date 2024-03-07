using System.Net;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly DatabaseContext _context;
        //private readonly DbSet<Users> _dbSet;
        public UsersRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            // _dbSet = _context.Set<Users>();
        }
        public async Task<object> GetAll(Guid id)
        {
            var response = await _context.Users.FindAsync(id);

            if(response != null)
            {
                return response;
            }

            return HttpStatusCode.NoContent;
        }
        // public async Task<Users> GetByEmailWithRolesAsync(Guid id)
        // {
        //     return await _context.Users  // Include the User entity if needed
        //         .Include(uc => uc.UserRoles)
        //             .ThenInclude(ur => ur.Role)
        //         .FirstOrDefaultAsync(x => x.UserId == id);
        // }

        //public IUsersRepository Users => new UsersRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Users> GetByEmailId(string emailId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailId == emailId);
        }
        public async Task<Users> GetByPhoneNumber(string mobileNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.MobileNumber == mobileNumber);
        }
    }
}
