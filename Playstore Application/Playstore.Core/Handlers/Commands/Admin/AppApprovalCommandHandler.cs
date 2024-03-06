using System.Net;
using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    public class AppApprovalCommand : IRequest<HttpStatusCode>
    {
        public AppPublishDto AppPublishDto { get; set; }

        public AppApprovalCommand(AppPublishDto publishDto)
        {
            this.AppPublishDto = publishDto;
        }
    }

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
            try
            {
                var response = await repository.UserRole.ApproveApp(request.AppPublishDto);

                Console.WriteLine(response);
    
                if(response != HttpStatusCode.NoContent && response != HttpStatusCode.Created)
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