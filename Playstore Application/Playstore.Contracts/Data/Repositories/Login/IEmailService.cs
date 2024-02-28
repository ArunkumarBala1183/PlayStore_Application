using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IEmailService 
    {
        Task SendOtpAsync(string email, string otp);
        Task SendUserCredentialsAsync(string email, string name, string mobileNumber,DateOnly dateOfBirth);
    }
}
