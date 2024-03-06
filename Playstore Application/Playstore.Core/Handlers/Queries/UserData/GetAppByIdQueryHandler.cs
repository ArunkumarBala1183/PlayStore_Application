using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppByIdValueQuery : IRequest<IEnumerable<AppInfoDetailsDTO>>
    {
        public Guid AppId { get; }
        public GetAppByIdValueQuery(Guid appId)
        {
            AppId = appId;
        }
    }

    public class GetAppByIdQueryHandler : IRequestHandler<GetAppByIdValueQuery,IEnumerable<AppInfoDetailsDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAppByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppInfoDetailsDTO>> Handle(GetAppByIdValueQuery request, CancellationToken cancellationToken)
        {
            // GuidDTO guidDTO=request.AppId;
            var app = await _repository.AppDetails.GetAppDetails(request.AppId);

            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.AppId}");
            }

            return (IEnumerable<AppInfoDetailsDTO>)app;
        }
    }
}