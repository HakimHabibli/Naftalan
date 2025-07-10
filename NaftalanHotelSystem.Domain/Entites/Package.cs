using NaftalanHotelSystem.Domain.Common;

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
