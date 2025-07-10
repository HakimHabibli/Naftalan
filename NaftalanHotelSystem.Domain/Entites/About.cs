using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Domain.Entites;

public class About : BaseEntity
{ 
    public string VideoLink { get; set; }
   
    
    //public string 
    //TODO:SEKIL OLACAQ BURDAA 


    public ICollection<AboutTranslation> AboutTranslations { get; set; }   
}
