using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
namespace NaftalanHotelSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromQuery] string to, [FromQuery] string subject, [FromQuery] string message)
    {
        await _notificationService.SendEmailAsync(to, subject, message);
        return Ok("Email göndərildi.");
    }
    [HttpPost("send-reservation-confirmation")]
    public async Task<IActionResult> SendReservationConfirmationEmail([FromBody] ReservationConfirmationDto data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }   
        
        await _notificationService.SendReservationConfirmationEmailAsync(data.Email, data);
        return Ok("Rezervasiya təsdiqi maili uğurla göndərildi.");
     
    }
}
