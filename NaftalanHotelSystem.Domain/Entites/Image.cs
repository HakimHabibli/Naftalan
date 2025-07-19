using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class Image :BaseEntity
{
    public string Url { get; set; }

    public ImageEntity Entity { get; set; }

    public int RelatedEntityId { get; set; }
}
