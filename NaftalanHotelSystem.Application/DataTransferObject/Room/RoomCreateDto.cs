using Microsoft.AspNetCore.Http;

namespace NaftalanHotelSystem.Application.DataTransferObject.Room;

public class RoomCreateDto
{
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public string? YoutubeVideoLink { get; set; }


    public List<RoomPriceByOccupancyDto> PricesByOccupancy { get; set; } = new List<RoomPriceByOccupancyDto>();
    public List<RoomTranslationCreateDto> Translations { get; set; } = new List<RoomTranslationCreateDto>();
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<IFormFile>? ImageFiles { get; set; }
    public List<int> ChildIds { get; set; }
}
public class RoomPriceByOccupancyDto
{
    public short Occupancy { get; set; }
    public double Price { get; set; }
}