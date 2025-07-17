using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject.Room;

public class RoomTranslationCreateDto
{
    public string Service { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MiniDescription { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MiniTitle { get; set; } = string.Empty;
    public Language Language { get; set; }
}
