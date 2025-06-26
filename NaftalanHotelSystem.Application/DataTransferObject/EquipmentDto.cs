using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class EquipmentDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
}
public class EquipmentCreateDto
{
    public List<EquipmentTranslationCreateDto> Translations { get; set; }
}
public class EquipmentUpdateDto
{
    public int Id { get; set; }
    public List<EquipmentTranslationCreateDto> Translations { get; set; }
}

public class EquipmentTranslationCreateDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}

public class TreatmentMethodDto //Getid,GetAll
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public Language Language { get; set; }
    public string Description { get; set; }
}
public class TreatmentMethodTranslationDto //create
{
    public string Name { get; set; }
    public Language Language { get; set; }
    public string Description { get; set; }


}
public class TrearmentMethodCreateDto//Create
{
    public List<TreatmentMethodTranslationDto> TreatmentMethodTranslationDtos { get; set; }
}
public class TrearmentMethodWriteDto//Update
{ 
    public int Id { get; set; }
    public List<TreatmentMethodTranslationDto> TreatmentMethodTranslationDtos { get; set; }
}