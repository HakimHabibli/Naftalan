using NaftalanHotelSystem.Application.DataTransferObject.Common;

namespace NaftalanHotelSystem.Application.DataTransferObject.About.Abstracts;

public abstract class AboutBaseTranslationDto : BaseTranslationDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string MiniTitle { get; set; }
    public string Description { get; set; }
}
