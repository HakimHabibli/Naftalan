using Microsoft.AspNetCore.Http;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class AboutUpdateDto
{
    public int Id { get; set; }
    public string VideoLink { get; set; }
    public IFormFile ImageFile { get; set; }

    public List<AboutTranslationUpdateDto> Translations { get; set; }
}

