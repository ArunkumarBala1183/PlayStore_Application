using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task CommitAsync();
        //Task<UserRole> GetByUserId(Guid userId);
        // Task<Users> GetByEmailId(string emailId);
        // Task<Users> GetByPhoneNumber(string mobileNumber);
        // Task<List<Users>> GetAll();
    }
}