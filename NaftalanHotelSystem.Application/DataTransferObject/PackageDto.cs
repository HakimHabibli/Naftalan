using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class PackageDto//Get,GetById
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public short DurationDay { get; set; }
    public string RoomType { get; set; }

    public List<PackageTranslationDto> PackageTranslations { get; set; }
    public List<TreatmentMethodDto> TreatmentMethods { get; set; }
}
public class PackageCreateDto //Create ,Update
{
    public string Name { get; set; }
    public double Price { get; set; }
    public short DurationDay { get; set; }
    public string RoomType { get; set; }
    public List<PackageTranslationDto> PackageTranslations { get; set; }
    public List<int> TreatmentMethodsIds { get; set; }
}


public class PackageTranslationDto 
{
    public string Description { get; set; }
    public Language Language { get; set; }
}

/*
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


  public int PackageId { get; set; }
  public Package Packages { get; set; }
}
   */

