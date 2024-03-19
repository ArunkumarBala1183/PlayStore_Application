using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppDataQuery : IRequest<IEnumerable<AppDownloadDataDto>>
    {
        public Guid AppId { get; set; }
        public Guid UserId {get;set;}
        public GetAppDataQuery(Guid appId, Guid userId)
        {
            this.AppId = appId;

            this.UserId = userId;

        }
    }

    public class GetAppDataQueryHandler : IRequestHandler<GetAppDataQuery, IEnumerable<AppDownloadDataDto>>
    {
        private readonly IUnitOfWork _repository;


        public GetAppDataQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<AppDownloadDataDto>> Handle(GetAppDataQuery request, CancellationToken cancellationToken)
        {
            if(request!=null){
            var app = await _repository.AppValue. GetAppData(request.AppId , request.UserId);
            if (app == null)
            {
                throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
            }
            var AppFile = new AppDownloadDataDto
            {
                appFile = app.AppFile
            };

            return new List<AppDownloadDataDto> { AppFile };
            }
           
                throw new ObjectNullException(Dataconstant.ObjectNullException);
            
            

        }


    }
}