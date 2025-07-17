using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject.Equipment;

public abstract class EquipmentBaseDto 
{
    public string Name { get; set; }
    public Language Language { get; set; }
}