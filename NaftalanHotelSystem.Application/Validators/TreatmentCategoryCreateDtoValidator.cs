using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Application.DataTransferObject.TreatmentMethod;

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


public class TreatmentMethodTranslationCreateDtoValidator : AbstractValidator<TreatmentMethodTranslationCreateDto>
{
    public TreatmentMethodTranslationCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad maksimum 100 simvol ola bilər."); 

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(1000).WithMessage("Təsvir maksimum 1000 simvol ola bilər."); 
    }
}

public class TreatmentMethodTranslationUpdateDtoValidator : AbstractValidator<TreatmentMethodTranslationUpdateDto>
{
    public TreatmentMethodTranslationUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(100).WithMessage("Ad maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(1000).WithMessage("Təsvir maksimum 1000 simvol ola bilər.");
    }
}


public class TreatmentMethodCreateDtoValidator : AbstractValidator<TreatmentMethodCreateDto>
{
    public TreatmentMethodCreateDtoValidator()
    {
        RuleFor(x => x.Translations) 
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");

        
        RuleForEach(x => x.Translations).SetValidator(new TreatmentMethodTranslationCreateDtoValidator());

        
        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");

    
        
        RuleFor(x => x.ImageFile)
            .Must(BeAValidImageSize).When(x => x.ImageFile != null) 
                .WithMessage("Şəkil faylının ölçüsü 5MB-dan çox olmamalıdır.")
            .Must(BeAValidImageExtension).When(x => x.ImageFile != null)
                .WithMessage("Yalnız JPG, JPEG, PNG formatlı şəkillərə icazə verilir.");
     
    }

  
    private bool HaveUniqueLanguages(List<TreatmentMethodTranslationCreateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }

  
    private bool BeAValidImageSize(IFormFile file)
    {
        return file.Length <= 11 * 1024 * 1024;
    }

    
    private bool BeAValidImageExtension(IFormFile file)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}


public class TreatmentMethodUpdateDtoValidator : AbstractValidator<TreatmentMethodUpdateDto>
{
    public TreatmentMethodUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Translations) 
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");

      
        RuleForEach(x => x.Translations).SetValidator(new TreatmentMethodTranslationUpdateDtoValidator());

     
        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");

     
        RuleFor(x => x.ImageFile)
            .Must(BeAValidImageSize).When(x => x.ImageFile != null)
                .WithMessage("Şəkil faylının ölçüsü 5MB-dan çox olmamalıdır.")
            .Must(BeAValidImageExtension).When(x => x.ImageFile != null)
                .WithMessage("Yalnız JPG, JPEG, PNG formatlı şəkillərə icazə verilir.");
    }

  
    private bool HaveUniqueLanguages(List<TreatmentMethodTranslationUpdateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }

  
    private bool BeAValidImageSize(IFormFile file)
    {
        return file.Length <= 11 * 1024 * 1024;
    }

  
    private bool BeAValidImageExtension(IFormFile file)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
      
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}
