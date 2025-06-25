using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class EquipmentTranslation :BaseEntity
{
    public string Name { get; set; }
    public Language Language { get; set; }

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
}