using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Infrastructure.Services;

namespace NaftalanHotelSystem.Infrastructure;

public class NotificationService: INotificationService
{
    private readonly SmtpSettings _smtpSettings;

    public NotificationService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Naftalan", _smtpSettings.User));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        //TODO:MessageBody Html + Css yazılacaq
        message.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
