using MediatR;
using Playstore.Contracts.DTO;
 
namespace Playstore.Providers.Handlers.Commands
{
public class ValidateOtpCommand : IRequest<bool>
{
    public ValidateOtpDTO Model { get; }
    public string ResetPasswordEmail { get; }
    public string ResetPasswordOTP { get; }
 
    public ValidateOtpCommand(ValidateOtpDTO model, string resetPasswordEmail, string resetPasswordOTP)
    {
        Model = model;
        ResetPasswordEmail = resetPasswordEmail;
        ResetPasswordOTP = resetPasswordOTP;
    }
}
 
 
 
}