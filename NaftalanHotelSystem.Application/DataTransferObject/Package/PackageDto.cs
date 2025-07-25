﻿using NaftalanHotelSystem.Application.DataTransferObject.TreatmentMethod;
using NaftalanHotelSystem.Domain.Enums;

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
public class TreatmentMethodDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
    public string Description { get; set; }
}
public class TreatmentMethodTranslationGetByIdDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}