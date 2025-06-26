namespace NaftalanHotelSystem.Application.DataTransferObject;

public class RoomCreateDto
{
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public string Picture { get; set; }
    public string YoutubeVideoLink { get; set; }

    public List<RoomTranslationCreateDto> Translations { get; set; }
    public List<int> EquipmentIds { get; set; }
}
