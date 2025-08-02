using Microsoft.AspNetCore.Http;

namespace NaftalanHotelSystem.Application.DataTransferObject.Room;

public class RoomUpdateDto
{
    public int Id { get; set; }
    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }
    public string? YoutubeVideoLink { get; set; }

    public List<RoomTranslationUpdateDto> Translations { get; set; } = new List<RoomTranslationUpdateDto>();
    public List<int> EquipmentIds { get; set; } = new List<int>();
    public List<int>? ImageIdsToDelete { get; set; }
    public List<IFormFile>? NewImageFiles { get; set; }
    public List<int> ChildIds { get; set; }
}
