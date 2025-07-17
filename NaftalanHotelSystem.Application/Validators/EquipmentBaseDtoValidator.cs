using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.Equipment;

namespace NaftalanHotelSystem.Application.Validators;

public abstract class EquipmentBaseDtoValidator<T> : AbstractValidator<T> where T : EquipmentBaseDto
{
    public EquipmentBaseDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class EquipmentCreateDtoValidator : AbstractValidator<EquipmentCreateDto>
{
    public EquipmentCreateDtoValidator()
    {
        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new EquipmentTranslationCreateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<EquipmentTranslationCreateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class EquipmentDtoValidator : EquipmentBaseDtoValidator<EquipmentDto>
{
    public EquipmentDtoValidator()
    {
       
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");
    }
}

public class EquipmentTranslationCreateDtoValidator : EquipmentBaseDtoValidator<EquipmentTranslationCreateDto>
{
    public EquipmentTranslationCreateDtoValidator()
    {
        
    }
}
public class EquipmentUpdateDtoValidator : AbstractValidator<EquipmentUpdateDto>
{
    public EquipmentUpdateDtoValidator()
    {
      
        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
      
        RuleForEach(x => x.Translations).SetValidator(new EquipmentTranslationCreateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<EquipmentTranslationCreateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
