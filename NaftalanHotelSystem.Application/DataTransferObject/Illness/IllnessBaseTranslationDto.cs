using NaftalanHotelSystem.Application.DataTransferObject.Common;

namespace NaftalanHotelSystem.Application.DataTransferObject.Illness;

public abstract class IllnessBaseTranslationDto : BaseTranslationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}