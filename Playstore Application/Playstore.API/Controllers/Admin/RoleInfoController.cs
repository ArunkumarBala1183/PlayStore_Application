using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class RoleInfoController
    {
        private readonly IMediator mediator;
        public RoleInfoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        
    }
}