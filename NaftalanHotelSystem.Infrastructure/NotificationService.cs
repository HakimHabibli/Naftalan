using System.Reflection;
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

    public async Task SendReservationConfirmationEmailAsync(string toEmail, ReservationConfirmationDto data)
    {
        string emailBody = string.Empty;
        var assembly = Assembly.GetExecutingAssembly();

        string resourceName = "NaftalanHotelSystem.Infrastructure.Templates.ReservationConfirmation.html";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                
                throw new FileNotFoundException($"Embedded resource '{resourceName}' not found. Make sure the file is set as 'Embedded Resource' and the namespace is correct.");
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                emailBody = await reader.ReadToEndAsync();
            }
        }


        emailBody = emailBody.Replace("${finalData.name}", data.Name ?? "")
                         .Replace("${finalData.surname}", data.Surname ?? "")
                         .Replace("${finalData.selectedRoom}", data.SelectedRoom ?? "")
                         .Replace("${finalData.date}", data.Date ?? "Seçilməyib") 
                         .Replace("${finalData.dayCount}", data.DayCount.ToString())
                         .Replace("${finalData.roomCount}", data.RoomCount.ToString())
                         .Replace("${finalData.guest}", data.Guest.ToString())
                         .Replace("${finalData.childCount}", data.ChildCount.ToString())
                         .Replace("${finalData.phoneNumber}", data.PhoneNumber ?? "")
                         .Replace("${finalData.email}", data.Email ?? "")
                         .Replace("${finalData.message}", string.IsNullOrEmpty(data.Message) ? "—" : data.Message)
                         .Replace("${price}", data.Price.ToString("F2"));


        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Park Naftalan Sanatoriyası", _smtpSettings.User));

       
        message.To.Add(MailboxAddress.Parse(toEmail));

       
        if (!string.Equals(toEmail, _smtpSettings.User, StringComparison.OrdinalIgnoreCase))
        {
            message.To.Add(MailboxAddress.Parse(_smtpSettings.User));
        }

        message.Subject = "Rezervasiya Təsdiqi: Park Naftalan Sanatoriyası";
        message.Body = new BodyBuilder { HtmlBody = emailBody }.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Password);
            await client.SendAsync(message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
