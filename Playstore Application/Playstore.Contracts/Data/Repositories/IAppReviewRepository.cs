using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppReviewRepository : IRepository<AppReview> { 
        Task<object> GetReview(Guid id);
    }
}