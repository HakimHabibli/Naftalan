namespace NaftalanHotelSystem.Application.DataTransferObject;

public class AboutDto
{
    public int Id { get; set; }
    public string VideoLink { get; set; }

    public string ImageUrl { get; set; }

    public List<AboutTranslationDto> Translations { get; set; }
}

