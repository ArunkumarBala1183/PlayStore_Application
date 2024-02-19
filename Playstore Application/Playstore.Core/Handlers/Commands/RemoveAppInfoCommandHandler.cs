using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
    public class RemoveAppInfoCommand : IRequest<HttpStatusCode>
    {
        public Guid id;
        public RemoveAppInfoCommand(Guid id)
        {
            this.id = id;
        }
    }
    public class RemoveAppInfoCommandHandler : IRequestHandler<RemoveAppInfoCommand, HttpStatusCode>
    {
        private readonly IAppInfoRepository repository;
        public RemoveAppInfoCommandHandler(IAppInfoRepository repository)
        {
            this.repository = repository;
        }
        public async Task<HttpStatusCode> Handle(RemoveAppInfoCommand request, CancellationToken cancellationToken)
        {
            return await this.repository.RemoveApp(request.id);
        }
    }
}