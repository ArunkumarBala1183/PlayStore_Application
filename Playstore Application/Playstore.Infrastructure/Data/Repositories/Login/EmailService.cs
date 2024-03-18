using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.EmailConfig;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Serilog;

public class EmailService : IEmailService
{
    private readonly EmailConfig _configuration;
    private readonly ILogger logger;

    public EmailService(IOptions<EmailConfig> configuration , IHttpContextAccessor httpContext)
    {
        _configuration = configuration.Value;
        logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(EmailService).Name);
    }
    public async Task SendOtpAsync(string email, string otp)
    {
        var senderEmail = new MailAddress(_configuration.Sender, _configuration.DisplayName);
        var receiverEmail = new MailAddress(email);
        var password = _configuration.Password;
        var subject = _configuration.OTPsubject;
        var body = _configuration.OtpEmailBody
        .Replace("{username}", email)
        .Replace("{otp}", otp);
        var smtp = new SmtpClient
        {
            Host = _configuration.Host,
            Port = _configuration.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(senderEmail.Address, password)
        };
        using (var message = new MailMessage(senderEmail, receiverEmail)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
        await Task.Delay(0);
        logger.Information($"Sending OTP to {email}");
    }
    public async Task SendUserCredentialsAsync(string email, string name, string mobileNumber,DateOnly dateOfBirth)
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
        var smtp = new SmtpClient
        {
            Host = _configuration.Host,
            Port = _configuration.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(senderEmail.Address, password)
        };
        using (var message = new MailMessage(senderEmail, receiverEmail)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
        await Task.Delay(0);
       logger.Information($"Sending user credentials email to {email}");
    }
}