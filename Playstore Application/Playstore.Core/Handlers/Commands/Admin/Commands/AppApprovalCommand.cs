using System.Net;
using MediatR;
using Playstore.Contracts.DTO.AppPublishRequest;

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
}