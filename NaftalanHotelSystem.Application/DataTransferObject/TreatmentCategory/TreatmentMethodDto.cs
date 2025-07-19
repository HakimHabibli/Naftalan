using NaftalanHotelSystem.Application.DataTransferObject.Package;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentMethodDto //Getid,GetAll
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public Language Language { get; set; }
    public string Description { get; set; }
}
public class TreatmentMethodGetByIdDto
{
    public int Id { get; set; }
    public List<TreatmentMethodTranslationGetByIdDto> Translations { get; set; } 
}
public class TreatmentMethodTranslationGetByIdDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}
