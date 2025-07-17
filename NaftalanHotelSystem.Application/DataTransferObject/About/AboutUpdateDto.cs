using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Application.DataTransferObject.About.Abstracts;

namespace NaftalanHotelSystem.Application.DataTransferObject.About;

public class AboutUpdateDto:AboutBaseDto
{
    public IFormFile ImageFile { get; set; }

    public List<AboutTranslationUpdateDto> Translations { get; set; }
}

