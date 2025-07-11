using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class RoomTranslationCreateDto
{
    public string Service { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MiniDescription { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MiniTitle { get; set; } = string.Empty;
    public Language Language { get; set; } // <-- RoomTranslation modelində Language enum olduğu üçün ENUM
}
public class RoomTranslationGetDto
{
    public int Id { get; set; } // RoomTranslation BaseEntity-dən gəldiyi üçün ID-si var
    public string Service { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MiniDescription { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MiniTitle { get; set; } = string.Empty;
    public Language Language { get; set; } // <-- RoomTranslation modelində Language enum olduğu üçün ENUM
}