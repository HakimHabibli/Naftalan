namespace NaftalanHotelSystem.API.ModelBinders;

public class BinableRoomCreateDto
{
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public List<IFormFile> ImageFiles { get; set; } // Fayllar birbaşa gəlir
    public string YoutubeVideoLink { get; set; }
    public string PricesByOccupancy { get; set; }
    public string Translations { get; set; } // JSON string olaraq gəlir
    public List<int> EquipmentIds { get; set; } // Bu da [FromForm] ilə gələ bilər
    public List<int> ChildIds { get; set; }
}
public class BinableRoomUpdateDto
{
    public int Id { get; set; }
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public string? YoutubeVideoLink { get; set; }

    public string Translations { get; set; } = string.Empty;
    public string? PricesByOccupancy { get; set; }
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<int> ChildIds { get; set; } = new List<int>();
    public List<IFormFile>? NewImageFiles { get; set; }
}