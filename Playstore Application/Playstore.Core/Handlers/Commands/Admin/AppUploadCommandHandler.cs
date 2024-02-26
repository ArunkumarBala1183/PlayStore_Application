using MediatR;
using Microsoft.AspNetCore.Http;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppData;

namespace Playstore.Providers.Handlers.Commands
{
    public class AppUploadCommand : IRequest<object>
    {
        public Guid AppId { get; set; }
        public IFormFile? AppFile { get; set; }
    }

    public class AppUploadCommandHandler : IRequestHandler<AppUploadCommand, object>
    {
        private readonly IAppDataRepository repository;

        public AppUploadCommandHandler(IAppDataRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(AppUploadCommand request, CancellationToken cancellationToken)
        {
            return await this.repository.UploadApp(request.AppFile , request.AppId);
        }
    }
}