using System.Net;
using MediatR;
using Microsoft.Extensions.Configuration;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppPublishRequest;

namespace Playstore.Providers.Handlers.Commands
{
    public class AppApprovalCommand : IRequest<object>
    {
        public AppPublishDto AppPublishDto { get; set; }

        public AppApprovalCommand(AppPublishDto publishDto)
        {
            this.AppPublishDto = publishDto;
        }
    }

    public class AppApprovalCommandHandler : IRequestHandler<AppApprovalCommand, object>
    {
        private readonly IUnitOfWork repository;

        public AppApprovalCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(AppApprovalCommand request, CancellationToken cancellationToken)
        {
            return await this.repository.UserRole.ApproveApp(request.AppPublishDto);
        }
    }
}