using System.ComponentModel.DataAnnotations;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface INotificationService 
{
    Task SendEmailAsync(string toEmail, string subject, string body);
    Task SendReservationConfirmationEmailAsync(string toEmail, ReservationConfirmationDto data);

}
public class ReservationConfirmationDto
{
    
    [Required(ErrorMessage = "Ad boş ola bilməz.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2 ilə 50 simvol arasında olmalıdır.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyad boş ola bilməz.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2 ilə 50 simvol arasında olmalıdır.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Email boş ola bilməz.")]
    [EmailAddress(ErrorMessage = "Düzgün email formatında olmalıdır.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Əlaqə nömrəsi boş ola bilməz.")]
    [Phone(ErrorMessage = "Düzgün telefon nömrəsi formatında olmalıdır.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Otaq seçimi boş ola bilməz.")]
    public string SelectedRoom { get; set; }

    [Required(ErrorMessage = "Tarix boş ola bilməz.")]
    public string Date { get; set; } 

    [Required(ErrorMessage = "Gün sayı boş ola bilməz.")]
    [Range(1, 365, ErrorMessage = "Gün sayı ən az 1 gün, ən çox 365 gün olmalıdır.")] 
    public int DayCount { get; set; }

    [Required(ErrorMessage = "Otaq sayı boş ola bilməz.")]
    [Range(1, 10, ErrorMessage = "Otaq sayı ən az 1, ən çox 10 olmalıdır.")]
    public int RoomCount { get; set; }

    [Required(ErrorMessage = "Qonaq sayı boş ola bilməz.")]
    [Range(1, 20, ErrorMessage = "Qonaq sayı ən az 1, ən çox 20 olmalıdır.")]
    public int Guest { get; set; }

   
    [Range(0, 10, ErrorMessage = "Uşaq sayı 0 ilə 10 arasında olmalıdır.")]
    public int ChildCount { get; set; }

    public string Message { get; set; } 

    [Required(ErrorMessage = "Qiymət boş ola bilməz.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Qiymət müsbət dəyər olmalıdır.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Dil seçimi boş ola bilməz.")]
    [StringLength(10, ErrorMessage = "Dil kodu çox uzundur.")]
    public string Language { get; set; } 
}