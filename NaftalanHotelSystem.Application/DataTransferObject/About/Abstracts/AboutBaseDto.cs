using NaftalanHotelSystem.Application.DataTransferObject.Common;

namespace NaftalanHotelSystem.Application.DataTransferObject.About.Abstracts;

public abstract class AboutBaseDto : BaseDto
{
    public string VideoLink { get; set; }
}