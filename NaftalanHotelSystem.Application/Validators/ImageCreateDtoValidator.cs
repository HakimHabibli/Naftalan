using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.Image;

namespace NaftalanHotelSystem.Application.Validators;

public class ImageCreateDtoValidator : AbstractValidator<ImageCreateDto>
{
    private const int MAX_FILE_SIZE_MB = 11; 
    private static readonly string[] ALLOWED_IMAGE_TYPES = { "image/jpeg", "image/png", "image/gif", "image/webp" };

    public ImageCreateDtoValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("Fayl boş ola bilməz.")
            .Must(file => file.Length > 0).WithMessage("Yüklənmiş fayl boş olmamalıdır.")
            .Must(file => file.Length <= MAX_FILE_SIZE_MB * 1024 * 1024).WithMessage($"Fayl maksimum {MAX_FILE_SIZE_MB}MB ola bilər.")
            .Must(file => ALLOWED_IMAGE_TYPES.Contains(file.ContentType)).WithMessage("Yalnız JPG, PNG, GIF və WEBP formatlı şəkillər qəbul olunur.");

        RuleFor(x => x.Entity)
            .IsInEnum().WithMessage("Entity növü etibarlı deyil.")
            .NotNull().WithMessage("Entity növü boş ola bilməz.");

        RuleFor(x => x.RelatedEntityId)
            .GreaterThan(0).WithMessage("Əlaqəli Entity ID-si sıfırdan böyük olmalıdır.");
    }
}
public class ImageDtoValidator : AbstractValidator<ImageDto>
{
    public ImageDtoValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("URL boş ola bilməz.")
            .Matches(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|bmp|webp))$").WithMessage("URL etibarlı şəkil linki olmalıdır.");

        RuleFor(x => x.Entity)
            .IsInEnum().WithMessage("Entity növü etibarlı deyil.")
            .NotNull().WithMessage("Entity növü boş ola bilməz.");

        RuleFor(x => x.RelatedEntityId)
            .GreaterThan(0).WithMessage("Əlaqəli Entity ID-si sıfırdan böyük olmalıdır.");
    }
}
public class ImageUpdateDtoValidator : AbstractValidator<ImageUpdateDto>
{
    private const int MAX_FILE_SIZE_MB = 11; 
    private static readonly string[] ALLOWED_IMAGE_TYPES = { "image/jpeg", "image/png", "image/gif", "image/webp" };

    public ImageUpdateDtoValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");
        RuleFor(x => x.File)
            .NotNull().WithMessage("Fayl boş ola bilməz.")
            .Must(file => file.Length > 0).WithMessage("Yüklənmiş fayl boş olmamalıdır.")
            .Must(file => file.Length <= MAX_FILE_SIZE_MB * 1024 * 1024).WithMessage($"Fayl maksimum {MAX_FILE_SIZE_MB}MB ola bilər.")
            .Must(file => ALLOWED_IMAGE_TYPES.Contains(file.ContentType)).WithMessage("Yalnız JPG, PNG, GIF və WEBP formatlı şəkillər qəbul olunur.");
    }
}
