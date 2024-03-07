using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Playstore.Providers.Handlers.Commands
{
    public class AppUploadCommand : IRequest<HttpStatusCode>
    {
        public Guid AppId { get; set; }
        public IFormFile? AppFile { get; set; }
    }

}