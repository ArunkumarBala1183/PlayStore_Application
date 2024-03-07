using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppDataQuery : IRequest<IEnumerable<AppDownloadDataDto>>
    {
        public Guid appId { get; set; }
        public Guid userId {get;set;}
        public GetAppDataQuery(Guid appId, Guid userId)
        {
            this.appId = appId;

            this.userId = userId;

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
            var app = await _repository.AppValue.GetAppData(request.appId , request.userId);
            if (app == null)
            {
                throw new EntityNotFoundException($"No App found for Id");
            }
            var AppFile = new AppDownloadDataDto
            {
                appFile = app.AppFile
            };

            return new List<AppDownloadDataDto> { AppFile };

        }


    }
}