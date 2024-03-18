using System.Net.Mail;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.EmailConfig;
using Playstore.Contracts.Data.Repositories;

public class EmailService : IEmailService
{
    private readonly EmailConfig _configuration;

    public EmailService(IOptions<EmailConfig> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task SendOtp(string email, string otp)
    {
        var senderEmail = new MailAddress(_configuration.Sender, _configuration.DisplayName);
        var receiverEmail = new MailAddress(email);
        var password = _configuration.Password;
        var subject = _configuration.OTPsubject;
        var body = _configuration.OtpEmailBody
            .Replace("{username}", email)
            .Replace("{otp}", otp);

        using (var message = new MailMessage(senderEmail, receiverEmail)
        {
            Subject = subject,
            Body = body
        })
        {
            using (var smtp = new SmtpClient(_configuration.Host, _configuration.Port))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(senderEmail.Address, password);
                await smtp.SendMailAsync(message);
            }
        }
    }

    public async Task SendUserCredentials(string email, string name, string mobileNumber, DateOnly dateOfBirth)
    {
        var subject = _configuration.Registersubject;
        var body = _configuration.UserCredentialsEmailBody
            .Replace("{name}", name)
            .Replace("{email}", email)
            .Replace("{dateOfBirth}", dateOfBirth.ToString())
            .Replace("{mobileNumber}", mobileNumber);

        var senderEmail = new MailAddress(_configuration.Sender, _configuration.DisplayName);
        var receiverEmail = new MailAddress(email);
        var password = _configuration.Password;

        using (var message = new MailMessage(senderEmail, receiverEmail)
        {
            Subject = subject,
            Body = body
        })
        {
            using (var smtp = new SmtpClient(_configuration.Host, _configuration.Port))
            {
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(senderEmail.Address, password);
                await smtp.SendMailAsync(message);
            }
        }
    }
}
