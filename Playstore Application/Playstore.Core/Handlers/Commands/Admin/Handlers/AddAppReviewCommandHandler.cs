using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
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