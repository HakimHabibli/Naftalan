using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.About;
using NaftalanHotelSystem.Application.DataTransferObject.About.Abstracts;
using NaftalanHotelSystem.Application.DataTransferObject.Child;

namespace NaftalanHotelSystem.Application.Validators;

public abstract class AboutBaseDtoValidator<T> : BaseDtoValidator<T> where T : AboutBaseDto
{
    public AboutBaseDtoValidator()
    {
        RuleFor(x => x.VideoLink)
            .NotEmpty().WithMessage("Video keçidi boş ola bilməz.")
            .Matches(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$").WithMessage("Video keçidi etibarlı YouTube linki olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.VideoLink)); 
    }
}

public class ChildCreateDtoValidator : AbstractValidator<ChildCreateDto>
{
    public ChildCreateDtoValidator()
    {
        RuleFor(x => x.AgeRange)
            .NotEmpty().WithMessage("Age range is required.")
            .Length(1, 50).WithMessage("Age range must be between 1 and 50 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

    }
}

public class ChildUpdateDtoValidator : AbstractValidator<ChildUpdateDto>
{
    public ChildUpdateDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required for update.");

        RuleFor(x => x.AgeRange)
            .NotEmpty().WithMessage("Age range is required.")
            .Length(1, 50).WithMessage("Age range must be between 1 and 50 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}


public abstract class AboutBaseTranslationDtoValidator<T> : BaseTranslationDtoValidator<T> where T : AboutBaseTranslationDto
{
    public AboutBaseTranslationDtoValidator()
    {
      
        RuleFor(x => x.Id) 
            .GreaterThan(0).WithMessage("Tərcümə ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MaximumLength(200).WithMessage("Başlıq maksimum 200 simvol ola bilər."); 

        RuleFor(x => x.MiniTitle)
            .NotEmpty().WithMessage("Qısa başlıq boş ola bilməz.")
            .MaximumLength(100).WithMessage("Qısa başlıq maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(1000).WithMessage("Təsvir maksimum 1000 simvol ola bilər.");
    }
}
public class AboutDtoValidator : AboutBaseDtoValidator<AboutDto>
{
    public AboutDtoValidator()
    {
     
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Şəkil URL-i boş ola bilməz.")
            //.Matches(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|bmp|webp))$").WithMessage("Şəkil URL-i etibarlı bir şəkil linki olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl)); 

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new AboutTranslationDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə mövcud olmalıdır.");
    }

    private bool HaveUniqueLanguages(List<AboutTranslationDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class AboutTranslationDtoValidator : AboutBaseTranslationDtoValidator<AboutTranslationDto>
{
    public AboutTranslationDtoValidator()
    {
        
    }
}
public class AboutTranslationUpdateDtoValidator : AboutBaseTranslationDtoValidator<AboutTranslationUpdateDto>
{
    public AboutTranslationUpdateDtoValidator()
    {
      
    }
}
public class AboutUpdateDtoValidator : AboutBaseDtoValidator<AboutUpdateDto>
{
    private const int MAX_FILE_SIZE_MB = 11;
    private static readonly string[] ALLOWED_IMAGE_TYPES = { "image/jpeg", "image/png", "image/gif" };

    public AboutUpdateDtoValidator()
    {
     

        RuleFor(x => x.ImageFile)
            .NotNull().WithMessage("Şəkil faylı boş ola bilməz.")
            .Must(file => file.Length > 0).WithMessage("Şəkil faylı boş ola bilməz.")
            .Must(file => file.Length <= MAX_FILE_SIZE_MB * 1024 * 1024).WithMessage($"Şəkil faylı maksimum {MAX_FILE_SIZE_MB}MB ola bilər.")
            .Must(file => ALLOWED_IMAGE_TYPES.Contains(file.ContentType)).WithMessage("Yalnız JPG, PNG və GIF şəkilləri qəbul olunur.")
            .When(x => x.ImageFile != null);

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new AboutTranslationUpdateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");
    }

    private bool HaveUniqueLanguages(List<AboutTranslationUpdateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
