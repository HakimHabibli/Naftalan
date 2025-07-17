using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject.Common;

public abstract class BaseTranslationDto 
{
    public Language Language { get; set; }
}