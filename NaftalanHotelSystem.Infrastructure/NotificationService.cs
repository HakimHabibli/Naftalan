using System.Reflection;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Infrastructure.Services;

namespace NaftalanHotelSystem.Infrastructure;

public class NotificationService : INotificationService
{
    private readonly SmtpSettings _smtpSettings;
    private readonly Dictionary<string, string> _subjectTranslations;
    // private readonly IWebHostEnvironment _webHostEnvironment; // Bu sətiri silin

    public NotificationService(IOptions<SmtpSettings> smtpSettings /*, IWebHostEnvironment webHostEnvironment */)
    {
        _smtpSettings = smtpSettings.Value;
        // _webHostEnvironment = webHostEnvironment; // Bu sətiri silin

        _subjectTranslations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "az", "Rezervasiya Təsdiqi: Park Naftalan Sanatoriyası" },
                { "en", "Reservation Confirmation: Park Naftalan Sanatorium" },
                { "ru", "Подтверждение Бронирования: Санаторий Парк Нафталан" }
            };
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Park Naftalan Sanatoriyası", _smtpSettings.User));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
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

        string languageCode = data.Language?.ToLowerInvariant() ?? "az";
        string baseTemplateFileName = "Reservation.html";

     
        string defaultNamespace = assembly.GetName().Name;

    
        string resourceName = $"{defaultNamespace}.Templates.{baseTemplateFileName}";

     
        string[] allResourceNames = assembly.GetManifestResourceNames();
        string actualResourceName = allResourceNames.FirstOrDefault(r => r.Equals(resourceName, StringComparison.OrdinalIgnoreCase) || r.EndsWith($".Templates.{baseTemplateFileName}", StringComparison.OrdinalIgnoreCase));

        if (actualResourceName == null)
        {
            throw new FileNotFoundException(
                $"Embedded resource '{baseTemplateFileName}' not found in assembly '{assembly.FullName}'. " +
                $"Attempted resource name: '{resourceName}'. " +
                $"Please ensure its 'Build Action' is set to 'Embedded resource' in the Infrastructure project. " +
                $"Available embedded resources: {string.Join(", ", allResourceNames)}"
            );
        }

        using (Stream stream = assembly.GetManifestResourceStream(actualResourceName))
        {
            if (stream == null)
            {
                throw new InvalidOperationException($"Could not get stream for embedded resource '{actualResourceName}'. This might indicate a corrupted assembly or file system issue.");
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                emailBody = await reader.ReadToEndAsync();
            }
        }

      
        string displayAz = (languageCode == "az") ? "block" : "none";
        string displayEn = (languageCode == "en") ? "block" : "none";
        string displayRu = (languageCode == "ru") ? "block" : "none";

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
                             .Replace("${price}", data.Price.ToString("F2"))
                             .Replace("${languageCode}", languageCode)
                             .Replace("${SubjectTranslation}", _subjectTranslations.GetValueOrDefault(languageCode, _subjectTranslations["az"]))
                             .Replace("${displayAz}", displayAz)
                             .Replace("${displayEn}", displayEn)
                             .Replace("${displayRu}", displayRu);


        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Park Naftalan Sanatoriyası", _smtpSettings.User));

        message.To.Add(MailboxAddress.Parse(toEmail));

        if (!string.Equals(toEmail, _smtpSettings.User, StringComparison.OrdinalIgnoreCase))
        {
            message.To.Add(MailboxAddress.Parse(_smtpSettings.User));
        }

        string subject = _subjectTranslations.GetValueOrDefault(languageCode, _subjectTranslations["az"]);
        message.Subject = subject;
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