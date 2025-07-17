using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.Illness;

namespace NaftalanHotelSystem.Application.Validators;

public abstract class IllnessBaseTranslationDtoValidator<T> : BaseTranslationDtoValidator<T> where T : IllnessBaseTranslationDto
{
    public IllnessBaseTranslationDtoValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(150).WithMessage("Ad maksimum 150 simvol ola bilər.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(1000).WithMessage("Təsvir maksimum 1000 simvol ola bilər.");
    }
}
public class IllnessCreateDtoValidator : AbstractValidator<IllnessCreateDto>
{
    public IllnessCreateDtoValidator()
    {
        RuleFor(x => x.TreatmentCategoryId)
            .GreaterThan(0).WithMessage("Müalicə kateqoriyası ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new IllnessTranslationCreateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<IllnessTranslationCreateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class IllnessDtoValidator : AbstractValidator<IllnessDto> 
{
    public IllnessDtoValidator()
    {

        RuleFor(x => x.Id)
          .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.TreatmentCategoryId)
            .GreaterThan(0).WithMessage("Müalicə kateqoriyası ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new IllnessTranslationDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə mövcud olmalıdır.");
    }

    private bool HaveUniqueLanguages(List<IllnessTranslationDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class IllnessTranslationCreateDtoValidator : IllnessBaseTranslationDtoValidator<IllnessTranslationCreateDto>
{
    public IllnessTranslationCreateDtoValidator()
    {
      
    }
}
public class IllnessTranslationDtoValidator : IllnessBaseTranslationDtoValidator<IllnessTranslationDto>
{
    public IllnessTranslationDtoValidator()
    {
       
    }
}
public class IllnessTranslationUpdateDtoValidator : IllnessBaseTranslationDtoValidator<IllnessTranslationUpdateDto>
{
    public IllnessTranslationUpdateDtoValidator()
    {
        
    }
}
public class IllnessUpdateDtoValidator : AbstractValidator<IllnessUpdateDto>
{
    public IllnessUpdateDtoValidator()
    {
        RuleFor(x => x.TreatmentCategoryId)
            .GreaterThan(0).WithMessage("Müalicə kateqoriyası ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new IllnessTranslationUpdateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<IllnessTranslationUpdateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
