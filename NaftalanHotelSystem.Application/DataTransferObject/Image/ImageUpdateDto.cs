using Microsoft.AspNetCore.Http;

namespace NaftalanHotelSystem.Application.DataTransferObject.Image;

public class ImageUpdateDto
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
}