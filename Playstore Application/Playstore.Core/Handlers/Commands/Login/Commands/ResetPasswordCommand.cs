using MediatR;
using Playstore.Contracts.DTO;
 
namespace Playstore.Providers.Handlers.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
 
        public PasswordResetDTO Model { get; }
        public string ResetPasswordEmail { get; }
        public string ResetPasswordOTP { get; }
 
 
        public ResetPasswordCommand(PasswordResetDTO model,string resetPasswordEmail, string resetPasswordOTP)
        {
            Model = model;
            ResetPasswordEmail = resetPasswordEmail;
            ResetPasswordOTP = resetPasswordOTP;
        }
    }
}