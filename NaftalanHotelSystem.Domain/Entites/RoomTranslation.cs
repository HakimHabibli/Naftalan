using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

/// <summary>
/// MultiLanguage for Room Class
/// </summary>
public class RoomTranslation:BaseEntity
{

    public string Service { get; set; }
    public string Description { get; set; }

    public string MiniDescription { get; set; }
    public string Title { get; set; }
    public string MiniTitle { get; set; }
   
    public Language Language { get; set; } 

    //TODO: Title Minidescription titlemini 
    
    public int RoomId  { get; set; }
    public Room Room { get; set; }
}
