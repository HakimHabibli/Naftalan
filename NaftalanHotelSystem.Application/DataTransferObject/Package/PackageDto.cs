namespace NaftalanHotelSystem.Application.DataTransferObject.Package;

public class PackageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public short DurationDay { get; set; }
    public string RoomType { get; set; }

    public List<PackageTranslationDto> PackageTranslations { get; set; }
    public List<TreatmentMethodDto> TreatmentMethods { get; set; }
}
