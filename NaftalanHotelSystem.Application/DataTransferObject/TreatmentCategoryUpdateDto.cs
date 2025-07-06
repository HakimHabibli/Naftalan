using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentCategoryUpdateDto
{
    public List<TreatmentCategoryTranslationUpdateDto> Translations { get; set; }
}

public class TreatmentCategoryTranslationUpdateDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
public class TreatmentCategoryCreateDto
{
    public List<TreatmentCategoryTranslationCreateDto> Translations { get; set; }
}

public class TreatmentCategoryTranslationCreateDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
public class TreatmentCategoryTranslationDto
{
    public string Name { get; set; }
    public Language Language { get; set; }
}
public class TreatmentCategoryDto
{
    public int Id { get; set; }
    public List<TreatmentCategoryTranslationDto> Translations { get; set; }
}
public class IllnessDto
{
    public int Id { get; set; }
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationDto> Translations { get; set; }
}
public class IllnessTranslationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}
public class IllnessCreateDto
{
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationCreateDto> Translations { get; set; }
}

public class IllnessTranslationCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}
public class IllnessUpdateDto
{
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationUpdateDto> Translations { get; set; }
}

public class IllnessTranslationUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}