using MediatR;
using Playstore.Contracts.DTO;

namespace Playstore.Providers.Handlers.Commands
{
    public class UpdatePermissionsCommand : IRequest<string>
    {
        public bool Allow { get; set; }
        //public UserCredentialsDTO Model { get; }
        public string RoleCode ="user";

        // public UpdatePermissionsCommand(UserCredentialsDTO model)
        // {
        //     this.Model = model;
        // }
    }
}