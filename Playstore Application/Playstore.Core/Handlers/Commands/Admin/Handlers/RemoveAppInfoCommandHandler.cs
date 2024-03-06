using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    public class RemoveAppInfoCommandHandler : IRequestHandler<RemoveAppInfoCommand, HttpStatusCode>
    {
        private readonly IAppInfoRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public RemoveAppInfoCommandHandler(IAppInfoRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<HttpStatusCode> Handle(RemoveAppInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.repository.RemoveApp(request.id);

                if (response != HttpStatusCode.NoContent)
                {
                    statusCodeHandler.HandleStatusCode(response);
                }

                return response;
            }
            catch (ApiResponseException)
            {

                throw;
            }
        }
    }
}