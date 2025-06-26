namespace NaftalanHotelSystem.Domain.Entites;

public class RoomEquipment 
{
    public int RoomId { get; set; }
    public Room Room { get; set; }

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
}
