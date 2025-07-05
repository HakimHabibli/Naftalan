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

    public int TreatmentCategoryId { get; set; }
    public TreatmentCategory Category { get; set; }
}
public class TreatmentCategory :BaseEntity
{
    public List<TreatmentMethodTranslation> TreatmentMethodTranslations { get; set; }

    public List<TreatmentCategoryTranslation> Translations { get; set; } 
  
}
public class TreatmentCategoryTranslation : BaseEntity
{
    public string Name { get; set; }
    public Language Language { get; set; }

    public int TreatmentCategoryId { get; set; }
    public TreatmentCategory TreatmentCategory { get; set; }
}
