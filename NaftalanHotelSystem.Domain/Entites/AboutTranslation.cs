using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Domain.Entites;

public class AboutTranslation : BaseEntity
{
    public string Title { get; set; }
    public string MiniTitle { get; set; }
    public string Description { get; set; }

    public Language Language { get; set; }

    public int AboutId { get; set; }
    public About About { get; set; }
}