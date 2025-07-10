using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Domain.Entites;

public class Image :BaseEntity
{
    public string Url { get; set; }

    public ImageEntity Entity { get; set; }

    public int RelatedEntityId { get; set; }
}
public enum ImageEntity 
{
    About = 1
}