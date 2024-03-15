using System.Net;
using MediatR;

namespace Playstore.Providers.Handlers.Commands
{
    public class RemoveAppInfoCommand : IRequest<HttpStatusCode>
    {
        public Guid Id { get; set; }
        public RemoveAppInfoCommand(Guid id)
        {
            this.Id = id;
        }
    }
}