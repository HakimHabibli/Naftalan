using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Domain.Entites;

public class About : BaseEntity
{ 
    public string VideoLink { get; set; }
    public ICollection<AboutTranslation> AboutTranslations { get; set; }   
}
