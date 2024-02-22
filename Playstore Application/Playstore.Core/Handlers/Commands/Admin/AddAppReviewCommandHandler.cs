using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Providers.Handlers.Commands
{
    public class AddAppReviewCommand : IRequest<object>
    {
        public Guid appReviewDto { get; set; }
        public AddAppReviewCommand(Guid appReview)
        {
            this.appReviewDto = appReview;
        }
    }

    public class AddAppReviewCommandHandler : IRequestHandler<AddAppReviewCommand, object>
    {
        private readonly IAppReviewRepository repository;
        public AddAppReviewCommandHandler(IAppReviewRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(AddAppReviewCommand request, CancellationToken cancellationToken)
        {
            return await this.repository.AddReview(request.appReviewDto);
        }
    }
}