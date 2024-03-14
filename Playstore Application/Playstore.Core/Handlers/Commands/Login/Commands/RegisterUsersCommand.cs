using MediatR;
using Playstore.Contracts.DTO;

namespace Playstore.Providers.Handlers.Commands
{
    public class RegisterUsersCommand : IRequest<Guid>
    {
        public RegisterUsersDTO Model { get; }
        public RegisterUsersCommand(RegisterUsersDTO model)
        {
            Model = model;
        }
    }
}