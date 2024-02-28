using MediatR;
using Playstore.Contracts.DTO;

namespace Playstore.Providers.Handlers.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {

        public PasswordResetDTO Model { get; }

        public ResetPasswordCommand(PasswordResetDTO model)
        {
            Model = model;
        }
    }
}