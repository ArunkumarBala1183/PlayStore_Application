using System.Net;
using MediatR;

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
}