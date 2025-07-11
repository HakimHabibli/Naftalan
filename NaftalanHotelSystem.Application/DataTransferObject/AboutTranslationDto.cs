using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class AboutTranslationDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string MiniTitle { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}

