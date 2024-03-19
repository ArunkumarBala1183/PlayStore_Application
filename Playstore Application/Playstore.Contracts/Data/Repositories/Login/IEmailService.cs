using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IEmailService 
    {
        Task SendOtp(string email, string otp);
        Task SendUserCredentials(string email, string name, string mobileNumber,DateOnly dateOfBirth);
    }
}
