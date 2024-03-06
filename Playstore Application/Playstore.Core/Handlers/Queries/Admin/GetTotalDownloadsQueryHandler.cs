using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.TotalDownloads;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetTotalDownloadsQuery : IRequest<DownloadDetailsDto>
    {

    }

    public class GetTotalDownloadsQueryHandler : IRequestHandler<GetTotalDownloadsQuery, DownloadDetailsDto>

    {
        private readonly IAppDownloadsRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetTotalDownloadsQueryHandler(IAppDownloadsRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<DownloadDetailsDto> Handle(GetTotalDownloadsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await repository.GetTotalDownloadsByDate();

                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode)response);
                }

                return (DownloadDetailsDto)response;
            }

            catch (ApiResponseException)
            {
                throw;
            }

        }
    }


}