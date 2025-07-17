using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentCategoryTranslationUpdateDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
