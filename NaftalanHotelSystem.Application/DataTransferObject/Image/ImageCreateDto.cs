using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.DataTransferObject.Image;

public class ImageCreateDto
{
    public IFormFile File { get; set; }
    public ImageEntity Entity { get; set; }
    public int RelatedEntityId { get; set; }
}
