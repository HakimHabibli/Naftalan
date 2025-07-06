using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Domain.Entites;
public class Room:BaseEntity
{
    //TODO:Picture propu class tamamlandiqdan sonra edilecek
    #region ToDo
    public string Picture { get; set; }
    //TODO: 4 dene sekil olacaq
    public string YoutubeVideoLink { get; set; }
    #endregion

    public string Category { get; set; }
    public short Area { get; set; }
    public double Price { get; set; }
    public short Member { get; set; }

 
    public ICollection<RoomEquipment> Equipments { get; set; }
    public ICollection<RoomTranslation> RoomTranslations { get; set; }
}
