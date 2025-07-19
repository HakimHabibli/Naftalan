using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentCategoryTranslationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
}
