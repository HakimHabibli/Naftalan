using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentCategoryTranslationCreateDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
