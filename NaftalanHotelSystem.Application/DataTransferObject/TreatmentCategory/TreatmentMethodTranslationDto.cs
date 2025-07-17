using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentMethodTranslationDto //create
{
    public string Name { get; set; }
    public Language Language { get; set; }
    public string Description { get; set; }

}
