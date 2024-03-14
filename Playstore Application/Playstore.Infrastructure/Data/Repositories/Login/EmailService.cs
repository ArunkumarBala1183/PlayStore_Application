using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;

public class EmailService : IEmailService
{
    private readonly EmailConfig _configuration;

    public EmailService(IOptions<EmailConfig> configuration)
    {
        _configuration = configuration.Value;
    }
    public async Task SendOtpAsync(string email, string otp)
    {
        var senderEmail = new MailAddress(_configuration.Sender, _configuration.DisplayName);
        var receiverEmail = new MailAddress(email);
        var password = _configuration.Password;
        var subject = _configuration.OTPsubject;
        var body = "Dear User \n\nGreetings From Authentication:)! \n\nHope you are doing well.\n\nWelcome to the Great Authentication Process. Your are trying to login the account by your credentials, with that please attach the provided OTP to access your account. Find your respected OTP below. \n\nUsername:" + email + "\n\nOTP:" + otp + "\n\nThank you.";
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
    }
    public async Task SendUserCredentialsAsync(string email, string name, string mobileNumber, DateOnly dateOfBirth)
    {
        var subject = _configuration.Registersubject;
        var body = $"Dear {name},\n\nCongratulations! You have successfully registered on our platform.\n\nYour credentials:\nEmail: {email}\nDateOfBirth: {dateOfBirth}\nMobile Number: {mobileNumber}\n\nThank you for joining!";
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
    }

}