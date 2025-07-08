using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class About : BaseEntity
{ 
    public string VideoLink { get; set; }
   
    
    //public string 
    //TODO:SEKIL OLACAQ BURDAA 

    public ICollection<AboutTranslation> AboutTranslations { get; set; }   
}
public class AboutTranslation : BaseEntity
{
    public string Title { get; set; }
    public string MiniTitle { get; set; }
    public string Description { get; set; }

    public Language Language { get; set; }

    public int AboutId { get; set; }
    public About About { get; set; }
}