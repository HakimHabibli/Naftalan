using NaftalanHotelSystem.Application.DataTransferObject.Child;

namespace NaftalanHotelSystem.Application.DataTransferObject.Room;

public class RoomGetDto
{
    public int Id { get; set; }
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public string? YoutubeVideoLink { get; set; }

    public List<RoomTranslationGetDto> Translations { get; set; } = new List<RoomTranslationGetDto>();
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<ChildGetDto> Children { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<RoomPriceByOccupancyDto> PricesByOccupancy { get; set; } = new List<RoomPriceByOccupancyDto>();
}
