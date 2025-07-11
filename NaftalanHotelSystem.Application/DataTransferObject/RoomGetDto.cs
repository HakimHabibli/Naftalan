namespace NaftalanHotelSystem.Application.DataTransferObject;

public class RoomGetDto
{
    public int Id { get; set; }
    public string Category { get; set; } // <-- Room modelində string olduğu üçün string
    public short Area { get; set; }     // <-- Room modelində short olduğu üçün short
    public double Price { get; set; }    // <-- Room modelində double olduğu üçün double
    public short Member { get; set; }   // <-- Room modelində short olduğu üçün short
    public string? YoutubeVideoLink { get; set; }

    public List<RoomTranslationGetDto> Translations { get; set; } = new List<RoomTranslationGetDto>();
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<string>? ImageUrls { get; set; } // Şəkil URL-ləri string olacaq
}
