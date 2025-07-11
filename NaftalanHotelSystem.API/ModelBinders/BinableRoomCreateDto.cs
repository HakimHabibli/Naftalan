namespace NaftalanHotelSystem.API.ModelBinders;

public class BinableRoomCreateDto
{
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public List<IFormFile> ImageFiles { get; set; } // Fayllar birbaşa gəlir
    public string YoutubeVideoLink { get; set; }
    public string Translations { get; set; } // JSON string olaraq gəlir
    public List<int> EquipmentIds { get; set; } // Bu da [FromForm] ilə gələ bilər
}
public class BinableRoomUpdateDto
{
    public int Id { get; set; } // Update zamanı otağın ID-si
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public string? YoutubeVideoLink { get; set; }

    // Translations JSON string kimi gələcək
    public string Translations { get; set; } = string.Empty;

    public List<int> EquipmentIds { get; set; } = new List<int>();

    public List<IFormFile>? NewImageFiles { get; set; } // Əlavə ediləcək yeni şəkillərin faylları
}