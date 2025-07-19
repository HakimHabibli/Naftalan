using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject.Equipment;

public class EquipmentTranslationCreateDto: EquipmentBaseDto
{
   
}


public class EquipmentGetDto
{
    public int Id { get; set; }
    public List<EquipmentTranslationCreateDto> Translations { get; set; }
}