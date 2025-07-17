using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.DataTransferObject.Image;

public class ImageDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ImageEntity Entity { get; set; }
    public int RelatedEntityId { get; set; }
}
