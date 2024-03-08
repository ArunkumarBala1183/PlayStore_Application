using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserDetailsRepository : IRepository<UsersDetailsDTO> 
    {
        Task<object> GetUsersDetails(Guid userId);
    }
}