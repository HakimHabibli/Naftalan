using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentCategoryTranslationDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
