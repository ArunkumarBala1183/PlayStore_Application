using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
    public class AppUploadCommandHandler : IRequestHandler<AppUploadCommand, HttpStatusCode>
    {
        private readonly IAppDataRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;

        public AppUploadCommandHandler(IAppDataRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<HttpStatusCode> Handle(AppUploadCommand request, CancellationToken cancellationToken)
        {
            var response = await this.repository.UploadApp(request.AppFile, request.AppId);

            if (response != HttpStatusCode.Created)
            {
                statusCodeHandler.HandleStatusCode(response);
            }

            return response;
        }
    }
}