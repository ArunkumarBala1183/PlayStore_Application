using System.Net;
using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
    public class AppApprovalCommandHandler : IRequestHandler<AppApprovalCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;

        public AppApprovalCommandHandler(IUnitOfWork repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<HttpStatusCode> Handle(AppApprovalCommand request, CancellationToken cancellationToken)
        {
            var response = await repository.UserRole.ApproveApp(request.AppPublishDto);
            
            if (response != HttpStatusCode.NoContent && response != HttpStatusCode.Created)
            {
                statusCodeHandler.HandleStatusCode(response);
            }

            return response;
        }
    }
}