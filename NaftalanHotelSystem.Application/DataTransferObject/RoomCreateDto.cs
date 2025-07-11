using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class RoomCreateDto
{
    public string Category { get; set; } // <-- Room modelində string olduğu üçün string
    public short Area { get; set; }     // <-- Room modelində short olduğu üçün short
    public double Price { get; set; }    // <-- Room modelində double olduğu üçün double
    public short Member { get; set; }   // <-- Room modelində short olduğu üçün short
    public string? YoutubeVideoLink { get; set; }

    public List<RoomTranslationCreateDto> Translations { get; set; } = new List<RoomTranslationCreateDto>();
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<IFormFile>? ImageFiles { get; set; }
}
public class RoomUpdateDto
{
    public int Id { get; set; }
    public string Category { get; set; } // <-- Room modelində string olduğu üçün string
    public short Area { get; set; }     // <-- Room modelində short olduğu üçün short
    public double Price { get; set; }    // <-- Room modelində double olduğu üçün double
    public short Member { get; set; }   // <-- Room modelində short olduğu üçün short
    public string? YoutubeVideoLink { get; set; }

    public List<RoomTranslationUpdateDto> Translations { get; set; } = new List<RoomTranslationUpdateDto>();
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<int>? ImageIdsToDelete { get; set; }
    public List<IFormFile>? NewImageFiles { get; set; }
}
public class RoomTranslationUpdateDto
{
    public int Id { get; set; }
    public string Service { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MiniDescription { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MiniTitle { get; set; } = string.Empty;
    public Language Language { get; set; } // <-- RoomTranslation modelində Language enum olduğu üçün ENUM
}