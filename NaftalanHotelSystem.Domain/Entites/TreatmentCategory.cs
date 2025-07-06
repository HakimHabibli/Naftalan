using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class TreatmentCategory :BaseEntity
{
   
    public List<TreatmentCategoryTranslation> Translations { get; set; } 
   public List<Illness> Illnesses { get; set; }
}
public class TreatmentCategoryTranslation : BaseEntity
{
    //TODO :Xestelikler var 
    public string Name { get; set; }
    public Language Language { get; set; }

    public int TreatmentCategoryId { get; set; }
    public TreatmentCategory TreatmentCategory { get; set; }
}
public class Illness : BaseEntity
{
    public int TreatmentCategoryId { get; set; }
    public TreatmentCategory TreatmentCategory { get; set; }  

    public List<IllnessTranslation> Translations { get; set; }
}

public class IllnessTranslation : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }

    public int IllnessId { get; set; }
    public Illness Illness { get; set; }
}