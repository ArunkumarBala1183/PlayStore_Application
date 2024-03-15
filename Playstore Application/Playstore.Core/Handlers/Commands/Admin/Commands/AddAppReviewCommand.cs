using MediatR;
using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Providers.Handlers.Commands
{
    public class AddAppReviewCommand : IRequest<object>
    {
        public AppReviewDto AppReviewDto { get; set; }
        public AddAppReviewCommand(AppReviewDto appReview)
        {
            this.AppReviewDto = appReview;
        }
    }
}