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
        public AppDownloadsDto appDownloadsDto { get; }

        public GetAppDataQuery(AppDownloadsDto _appDownloadsDto)
        {
            appDownloadsDto = _appDownloadsDto;

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
            AppDownloadsDto model = request.appDownloadsDto;
            var app = await _repository.AppValue.GetAppData(model);
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