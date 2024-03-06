namespace Playstore.Contracts.DTO.AppReview
{
    public record struct AppReviewDto
    (
         int Rating,
         string Comment,
         Guid AppId,
         Guid UserId
    );
}