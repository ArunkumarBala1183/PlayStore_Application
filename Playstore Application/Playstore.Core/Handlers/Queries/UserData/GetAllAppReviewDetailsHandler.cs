using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppReview;


namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAllAppReviewDetails : IRequest<IEnumerable<AppReviewDetailsDTO>>
    {
        public Guid AppId { get; }
        public GetAllAppReviewDetails(Guid appId)
        {
            AppId = appId;
        }
    }

    public class GetAllAppReviewDetailsHandler : IRequestHandler<GetAllAppReviewDetails, IEnumerable<AppReviewDetailsDTO>>
    {
        private readonly IUnitOfWork _repository;

        public GetAllAppReviewDetailsHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppReviewDetailsDTO>> Handle(GetAllAppReviewDetails request, CancellationToken cancellationToken)
        {
            var app = await _repository.AppReview.GetReview(request.AppId);

            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.AppId}");
            }

            return (IEnumerable<AppReviewDetailsDTO>)app;
        }


    }
}