using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppReviewRepository
    {
        Task<object> AddReview(Guid appReviewDetails);
    }
}