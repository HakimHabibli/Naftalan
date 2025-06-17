using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;
public class Room:BaseEntity
{
    //TODO:Picture propu class tamamlandiqdan sonra edilecek
    #region ToDo
    public string Picture { get; set; }
    public string YoutubeVideoLink { get; set; }
    #endregion

    public string Category { get; set; }//Adlandirma
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }



    public ICollection<RoomEquipment> Equipments { get; set; }
    public ICollection<RoomTranslation> RoomTranslations { get; set; }
}
public class RoomEquipment 
{
    public int RoomId { get; set; }
    public Room Room { get; set; }

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
}

public class Equipment :BaseEntity 
{
    public ICollection<EquipmentTranslation> EquipmentTranslations { get; set; }

    public ICollection<RoomEquipment> RoomEquipments { get; set; }
}

public class EquipmentTranslation :BaseEntity
{
    public string Name { get; set; }
    public Language Language { get; set; }

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
}