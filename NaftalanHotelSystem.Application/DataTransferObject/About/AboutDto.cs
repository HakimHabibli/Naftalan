using NaftalanHotelSystem.Application.DataTransferObject.About.Abstracts;

namespace NaftalanHotelSystem.Application.DataTransferObject.About;

public class AboutDto:AboutBaseDto
{
    public string ImageUrl { get; set; }
    public List<AboutTranslationDto> Translations { get; set; }
}
