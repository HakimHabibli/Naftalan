using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class Package : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public short DurationDay { get; set; }
    public string RoomType { get; set; }

    public List<PackageTranslation> PackageTranslations { get; set; }
    public List<TreatmentMethod> TreatmentMethods { get; set;}
}
public class PackageTranslation : BaseEntity 
{
    public string Description { get; set; }
    public Language Language { get; set; }

    public int PackageId { get; set; }
    public Package Packages { get; set; }
}