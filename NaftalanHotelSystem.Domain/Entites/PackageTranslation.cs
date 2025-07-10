using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class PackageTranslation : BaseEntity 
{
    public string Description { get; set; }
    public Language Language { get; set; }

    public int PackageId { get; set; }
    public Package Packages { get; set; }
}