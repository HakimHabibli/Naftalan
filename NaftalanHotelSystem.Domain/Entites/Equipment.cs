using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Domain.Entites;

public class Equipment :BaseEntity 
{
    public ICollection<EquipmentTranslation> EquipmentTranslations { get; set; }

    public ICollection<RoomEquipment> RoomEquipments { get; set; }
}
