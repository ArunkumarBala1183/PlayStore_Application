using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppDataQuery : IRequest<AppDownloadDataDto>
    {
        public Guid AppId { get; }
   
        public GetAppDataQuery(Guid appId)
        {
            AppId = appId;
           
        }
    }

    public class GetAppDataQueryHandler : IRequestHandler<GetAppDataQuery,AppDownloadDataDto>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
    

        public GetAppDataQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppDownloadDataDto> Handle(GetAppDataQuery request, CancellationToken cancellationToken)
        {
            var app = await _repository.AppValue.GetAppData(request.AppId);
            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.AppId}");
            }
           
            return _mapper.Map<AppDownloadDataDto>(app);
           
        }

        
    }
}