using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppReviewRepository : IRepository<AppReview> { 
        Task<object> GetReview(Guid AppId);
    }
}