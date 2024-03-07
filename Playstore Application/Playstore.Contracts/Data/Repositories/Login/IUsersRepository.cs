using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUsersRepository : IRepository<Users>
    {
        Task CommitAsync();
        //Task<Users> GetByEmailWithRolesAsync(Guid id);
        Task<Users> GetByEmailId(string emailId);
        Task<Users> GetByPhoneNumber(string mobileNumber);
        Task<object> GetAll(Guid id);
    }
}