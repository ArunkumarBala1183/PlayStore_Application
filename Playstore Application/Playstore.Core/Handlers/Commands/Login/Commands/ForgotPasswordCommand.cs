using MediatR;
using Playstore.Contracts.DTO;

namespace Playstore.Providers.Handlers.Commands
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public ForgotPasswordDTO Model { get; }

        public ForgotPasswordCommand(ForgotPasswordDTO model)
        {
            Model = model;
        }
    }
}