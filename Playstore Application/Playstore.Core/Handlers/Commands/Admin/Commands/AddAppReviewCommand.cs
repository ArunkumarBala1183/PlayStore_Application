using MediatR;
using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Providers.Handlers.Commands
{
    public class AddAppReviewCommand : IRequest<object>
    {
        public AppReviewDto appReviewDto { get; set; }
        public AddAppReviewCommand(AppReviewDto appReview)
        {
            this.appReviewDto = appReview;
        }
    }
}