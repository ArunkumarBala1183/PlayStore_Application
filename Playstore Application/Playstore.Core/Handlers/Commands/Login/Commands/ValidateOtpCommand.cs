using MediatR;
using Playstore.Contracts.DTO;

namespace Playstore.Providers.Handlers.Commands
{
    public class ValidateOtpCommand : IRequest<bool>
    {
        public ValidateOtpDTO Model { get; }

        public ValidateOtpCommand(ValidateOtpDTO model)
        {
            Model = model;
        }
    }
}