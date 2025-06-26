using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class TreatmentMethod:BaseEntity
{
    public List<Package> Packages { get; set; }
    public List<TreatmentMethodTranslation> Translations { get; set; }  
}
public class TreatmentMethodTranslation:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Language Language { get; set; }

    public int TreatmentMethodId { get; set; }
    public TreatmentMethod TreatmentMethod { get; set; }
}
