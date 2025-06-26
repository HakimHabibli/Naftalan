using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class RoomTranslationCreateDto
{
    public string Service { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}