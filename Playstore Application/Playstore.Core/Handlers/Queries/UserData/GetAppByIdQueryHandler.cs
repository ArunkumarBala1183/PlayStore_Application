using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppByIdValueQuery : IRequest<object>
    {
        public Guid AppId { get; }
        public GetAppByIdValueQuery(Guid appId)
        {
            AppId = appId;
        }
    }

    public class GetAppByIdQueryHandler : IRequestHandler<GetAppByIdValueQuery, object>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAppByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAppByIdValueQuery request, CancellationToken cancellationToken)
        {
            var app = await _repository.AppDetails.GetAppDetails(request.AppId);

            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.AppId}");
            }

            return app;
        }
    }
}