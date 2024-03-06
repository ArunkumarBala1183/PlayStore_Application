using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppDataQuery : IRequest<object>
    {
        public Guid AppId { get; }
   
        public GetAppDataQuery(Guid appId)
        {
            AppId = appId;
           
        }
    }

    public class GetAppDataQueryHandler : IRequestHandler<GetAppDataQuery, object>
    {
        private readonly IUnitOfWork _repository;
      
    

        public GetAppDataQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;
           
        }

        public async Task<object> Handle(GetAppDataQuery request, CancellationToken cancellationToken)
        {
            var app = await _repository.AppValue.GetAppData(request.AppId);
            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id {request.AppId}");
            }
           
            // return _mapper.Map<AppDownloadDataDto>(app);
            return app;
           
        }

        
    }
}