using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppInfoDownloadFile : IRequest<object>
    {
        public Guid Userid{get;set;}
        public GetAppInfoDownloadFile(Guid _Userid)
        {
          Userid=_Userid;
        }
    }

    public class GetAppInfoDownloadFileHandler : IRequestHandler<GetAppInfoDownloadFile,object>
    {
        private readonly IUnitOfWork _repository;

        public GetAppInfoDownloadFileHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetAppInfoDownloadFile request, CancellationToken cancellationToken)
        {
          

            var app = await _repository.AppDownload.GetData(request.Userid);

            if (app == null)
            {
                throw new EntityNotFoundException($"No App found");
            }
    
            return app;
        }
    }
}