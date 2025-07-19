using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject.TreatmentMethod;

public abstract class TreatmentMethodBaseTranslationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}


public class TreatmentMethodTranslationCreateDto : TreatmentMethodBaseTranslationDto
{
    
}


public class TreatmentMethodTranslationGetDto : TreatmentMethodBaseTranslationDto
{
    public int Id { get; set; } 
}


public class TreatmentMethodTranslationUpdateDto : TreatmentMethodBaseTranslationDto
{
    public int Id { get; set; } 
}


public class TreatmentMethodCreateDto
{
    
    public List<TreatmentMethodTranslationCreateDto> Translations { get; set; }
    public IFormFile ImageFile { get; set; } 
}

public class TreatmentMethodUpdateDto
{
    public int Id { get; set; } 
    public List<TreatmentMethodTranslationUpdateDto> Translations { get; set; }
    public IFormFile ImageFile { get; set; } 
}

public class TreatmentMethodGetByIdDto
{
    public int Id { get; set; }
    
    public List<TreatmentMethodTranslationGetDto> Translations { get; set; }
    public string ImageUrl { get; set; } 
}