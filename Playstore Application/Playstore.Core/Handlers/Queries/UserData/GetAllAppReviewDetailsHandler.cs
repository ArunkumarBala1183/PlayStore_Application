using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;


namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAllAppReviewDetails : IRequest<AppReview>
    {
        public Guid AppId { get; }
        public GetAllAppReviewDetails(Guid appId)
        {
            AppId = appId;
        }
    }

    public class GetAllAppReviewDetailsHandler : IRequestHandler<GetAllAppReviewDetails, AppReview>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllAppReviewDetailsHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppReview> Handle(GetAllAppReviewDetails request, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            var app = await _repository.AppReview.GetReview(request.AppId);

            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.AppId}");
            }
           
            return _mapper.Map<AppReview>(app);
        }

        // public async Task<AppInfo> Handle(GetAppByIdQuery request, CancellationToken cancellationToken)
        // {
        //     var app = await Task.FromResult(_repository.AppInfo.Get(request.AppId));

        //     if (app == null)
        //     {
        //         throw new EntityNotFoundException($"No App found for Id {request.AppId}");
        //     }

        //     return _mapper.Map<AppInfo>(app);
        // }
    }
}