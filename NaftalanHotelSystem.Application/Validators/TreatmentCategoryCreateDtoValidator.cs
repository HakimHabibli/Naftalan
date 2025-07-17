using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject;

namespace NaftalanHotelSystem.Application.Validators;

public class TreatmentCategoryCreateDtoValidator : AbstractValidator<TreatmentCategoryCreateDto>
{
    public TreatmentCategoryCreateDtoValidator()
    {
        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new TreatmentCategoryTranslationCreateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<TreatmentCategoryTranslationCreateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class TreatmentCategoryDtoValidator : AbstractValidator<TreatmentCategoryDto>
{
    public TreatmentCategoryDtoValidator()
    {
        RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new TreatmentCategoryTranslationDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə mövcud olmalıdır.");
    }

    private bool HaveUniqueLanguages(List<TreatmentCategoryTranslationDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class TreatmentCategoryTranslationCreateDtoValidator : AbstractValidator<TreatmentCategoryTranslationCreateDto>
{
    public TreatmentCategoryTranslationCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class TreatmentCategoryTranslationDtoValidator : AbstractValidator<TreatmentCategoryTranslationDto>
{
    public TreatmentCategoryTranslationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class TreatmentCategoryTranslationUpdateDtoValidator : AbstractValidator<TreatmentCategoryTranslationUpdateDto>
{
    public TreatmentCategoryTranslationUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class TreatmentCategoryUpdateDtoValidator : AbstractValidator<TreatmentCategoryUpdateDto>
{
    public TreatmentCategoryUpdateDtoValidator()
    {
   
        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new TreatmentCategoryTranslationUpdateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<TreatmentCategoryTranslationUpdateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class TrearmentMethodWriteDtoValidator : AbstractValidator<TrearmentMethodWriteDto>
{
    public TrearmentMethodWriteDtoValidator()
    {

        RuleFor(x => x.Id)
          .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.TreatmentMethodTranslationDtos)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.TreatmentMethodTranslationDtos).SetValidator(new TreatmentMethodTranslationDtoValidator());  

        RuleFor(x => x.TreatmentMethodTranslationDtos)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<TreatmentMethodTranslationDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class TreatmentMethodCreateDtoValidator : AbstractValidator<TreatmentMethodCreateDto>
{
    public TreatmentMethodCreateDtoValidator()
    {
        RuleFor(x => x.TreatmentMethodTranslationDtos)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.TreatmentMethodTranslationDtos).SetValidator(new TreatmentMethodTranslationDtoValidator()); 
        RuleFor(x => x.TreatmentMethodTranslationDtos)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<TreatmentMethodTranslationDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class TreatmentMethodDtoValidator : AbstractValidator<TreatmentMethodDto>
{
    public TreatmentMethodDtoValidator()
    {
        RuleFor(x => x.Id)
    .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(150).WithMessage("Ad maksimum 150 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(1000).WithMessage("Təsvir maksimum 1000 simvol ola bilər.");
    }
}
public class TreatmentMethodTranslationDtoValidator : AbstractValidator<TreatmentMethodTranslationDto>
{
    public TreatmentMethodTranslationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(150).WithMessage("Ad maksimum 150 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(1000).WithMessage("Təsvir maksimum 1000 simvol ola bilər.");
    }
}