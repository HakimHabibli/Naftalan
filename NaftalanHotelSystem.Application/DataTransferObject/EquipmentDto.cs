using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class EquipmentDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
}
public class EquipmentCreateDto
{
    public List<EquipmentTranslationCreateDto> Translations { get; set; }
}
public class EquipmentUpdateDto
{
    public List<EquipmentTranslationCreateDto> Translations { get; set; }
}

public class EquipmentTranslationCreateDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
public class ImageDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ImageEntity Entity { get; set; }
    public int RelatedEntityId { get; set; }
}

public class ImageCreateDto
{
    public IFormFile File { get; set; }
    public ImageEntity Entity { get; set; }
    public int RelatedEntityId { get; set; }
}

public class ImageUpdateDto
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
}