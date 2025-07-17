using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.Room;

namespace NaftalanHotelSystem.Application.Validators;

public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
{
    private const int MAX_IMAGE_FILE_SIZE_MB = 11;
    private static readonly string[] ALLOWED_IMAGE_TYPES = { "image/jpeg", "image/png", "image/gif", "image/webp" };

    public RoomCreateDtoValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Kateqoriya boş ola bilməz.")
            .MaximumLength(100).WithMessage("Kateqoriya maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Area)
            .GreaterThan((short)0).WithMessage("Sahə sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Qiymət sıfırdan kiçik ola bilməz.");

        RuleFor(x => x.Member)
            .GreaterThan((short)0).WithMessage("Üzv sayı sıfırdan böyük olmalıdır.");

        RuleFor(x => x.YoutubeVideoLink)
            .Matches(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$").WithMessage("YouTube video keçidi etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.YoutubeVideoLink));

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new RoomTranslationCreateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");

        RuleFor(x => x.EquipmentIds)
            .NotNull().WithMessage("Avadanlıq ID-ləri siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir avadanlıq ID-si olmalıdır.");
        RuleForEach(x => x.EquipmentIds)
            .GreaterThan(0).WithMessage("Avadanlıq ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.ImageFiles)
            .Must(files => files == null || files.Count <= 5).WithMessage("Maksimum 5 şəkil faylı yükləyə bilərsiniz.")
            .When(x => x.ImageFiles != null && x.ImageFiles.Any());

        RuleForEach(x => x.ImageFiles)
            .NotNull().WithMessage("Yüklənən şəkil faylı boş ola bilməz.")
            .Must(file => file.Length > 0).WithMessage("Yüklənən şəkil faylı boş olmamalıdır.")
            .Must(file => file.Length <= MAX_IMAGE_FILE_SIZE_MB * 1024 * 1024).WithMessage($"Yüklənən şəkil faylı maksimum {MAX_IMAGE_FILE_SIZE_MB}MB ola bilər.")
            .Must(file => ALLOWED_IMAGE_TYPES.Contains(file.ContentType)).WithMessage("Yalnız JPG, PNG, GIF və WEBP formatlı şəkillər qəbul olunur.")
            .When(x => x.ImageFiles != null && x.ImageFiles.Any());
    }

    private bool HaveUniqueLanguages(List<RoomTranslationCreateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class RoomGetDtoValidator : AbstractValidator<RoomGetDto> 
{
    public RoomGetDtoValidator()
    {
        RuleFor(x => x.Id)
     .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Kateqoriya boş ola bilməz.")
            .MaximumLength(100).WithMessage("Kateqoriya maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Area)
            .GreaterThan((short)0).WithMessage("Sahə sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Qiymət sıfırdan kiçik ola bilməz.");

        RuleFor(x => x.Member)
            .GreaterThan((short)0).WithMessage("Üzv sayı sıfırdan böyük olmalıdır.");

        RuleFor(x => x.YoutubeVideoLink)
            .Matches(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$").WithMessage("YouTube video keçidi etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.YoutubeVideoLink));

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new RoomTranslationGetDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə mövcud olmalıdır.");

        RuleFor(x => x.EquipmentIds)
            .NotNull().WithMessage("Avadanlıq ID-ləri siyahısı boş ola bilməz.");
        RuleForEach(x => x.EquipmentIds)
            .GreaterThan(0).WithMessage("Avadanlıq ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.ImageUrls)
            .NotNull().WithMessage("Şəkil URL-ləri siyahısı boş ola bilməz.");
        RuleForEach(x => x.ImageUrls)
            .NotEmpty().WithMessage("Şəkil URL-i boş ola bilməz.")
            .Matches(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|jpeg|gif|bmp|webp))$").WithMessage("Şəkil URL-i etibarlı bir şəkil linki olmalıdır.");
    }

    private bool HaveUniqueLanguages(List<RoomTranslationGetDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
public class RoomTranslationCreateDtoValidator : AbstractValidator<RoomTranslationCreateDto>
{
    public RoomTranslationCreateDtoValidator()
    {
        RuleFor(x => x.Service)
            .NotEmpty().WithMessage("Servis boş ola bilməz.")
            .MaximumLength(200).WithMessage("Servis maksimum 200 simvol ola bilər.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(500).WithMessage("Təsvir maksimum 500 simvol ola bilər.");

        RuleFor(x => x.MiniDescription)
            .NotEmpty().WithMessage("Qısa təsvir boş ola bilməz.")
            .MaximumLength(300).WithMessage("Qısa təsvir maksimum 300 simvol ola bilər.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MaximumLength(150).WithMessage("Başlıq maksimum 150 simvol ola bilər.");

        RuleFor(x => x.MiniTitle)
            .NotEmpty().WithMessage("Qısa başlıq boş ola bilməz.")
            .MaximumLength(100).WithMessage("Qısa başlıq maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class RoomTranslationGetDtoValidator : AbstractValidator<RoomTranslationGetDto>
{
    public RoomTranslationGetDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Service)
            .NotEmpty().WithMessage("Servis boş ola bilməz.")
            .MaximumLength(200).WithMessage("Servis maksimum 200 simvol ola bilər.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(500).WithMessage("Təsvir maksimum 500 simvol ola bilər.");

        RuleFor(x => x.MiniDescription)
            .NotEmpty().WithMessage("Qısa təsvir boş ola bilməz.")
            .MaximumLength(300).WithMessage("Qısa təsvir maksimum 300 simvol ola bilər.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MaximumLength(150).WithMessage("Başlıq maksimum 150 simvol ola bilər.");

        RuleFor(x => x.MiniTitle)
            .NotEmpty().WithMessage("Qısa başlıq boş ola bilməz.")
            .MaximumLength(100).WithMessage("Qısa başlıq maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class RoomTranslationUpdateDtoValidator : AbstractValidator<RoomTranslationUpdateDto>
{
    public RoomTranslationUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Service)
            .NotEmpty().WithMessage("Servis boş ola bilməz.")
            .MaximumLength(200).WithMessage("Servis maksimum 200 simvol ola bilər.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Təsvir boş ola bilməz.")
            .MaximumLength(500).WithMessage("Təsvir maksimum 500 simvol ola bilər.");

        RuleFor(x => x.MiniDescription)
            .NotEmpty().WithMessage("Qısa təsvir boş ola bilməz.")
            .MaximumLength(300).WithMessage("Qısa təsvir maksimum 300 simvol ola bilər.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MaximumLength(150).WithMessage("Başlıq maksimum 150 simvol ola bilər.");

        RuleFor(x => x.MiniTitle)
            .NotEmpty().WithMessage("Qısa başlıq boş ola bilməz.")
            .MaximumLength(100).WithMessage("Qısa başlıq maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Language)
            .IsInEnum().WithMessage("Dil dəyəri etibarlı deyil.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
public class RoomUpdateDtoValidator : AbstractValidator<RoomUpdateDto> 
{
    private const int MAX_NEW_IMAGE_FILE_SIZE_MB = 11;
    private static readonly string[] ALLOWED_IMAGE_TYPES = { "image/jpeg", "image/png", "image/gif", "image/webp" };

    public RoomUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Kateqoriya boş ola bilməz.")
            .MaximumLength(100).WithMessage("Kateqoriya maksimum 100 simvol ola bilər.");

        RuleFor(x => x.Area)
            .GreaterThan((short)0).WithMessage("Sahə sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Qiymət sıfırdan kiçik ola bilməz.");

        RuleFor(x => x.Member)
            .GreaterThan((short)0).WithMessage("Üzv sayı sıfırdan böyük olmalıdır.");

        RuleFor(x => x.YoutubeVideoLink)
            .Matches(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$").WithMessage("YouTube video keçidi etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.YoutubeVideoLink));

        RuleFor(x => x.Translations)
            .NotNull().WithMessage("Tərcümələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir tərcümə olmalıdır.");
        RuleForEach(x => x.Translations).SetValidator(new RoomTranslationUpdateDtoValidator());

        RuleFor(x => x.Translations)
            .Must(HaveUniqueLanguages)
            .WithMessage("Hər dil üçün yalnız bir tərcümə daxil edilə bilər.");

        RuleFor(x => x.EquipmentIds)
            .NotNull().WithMessage("Avadanlıq ID-ləri siyahısı boş ola bilməz.");
        RuleForEach(x => x.EquipmentIds)
            .GreaterThan(0).WithMessage("Avadanlıq ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.ImageIdsToDelete)
            .NotNull().WithMessage("Silinəcək şəkil ID-ləri siyahısı boş ola bilməz.");
        RuleForEach(x => x.ImageIdsToDelete)
            .GreaterThan(0).WithMessage("Silinəcək şəkil ID-si sıfırdan böyük olmalıdır.");

        RuleFor(x => x.NewImageFiles)
            .Must(files => files == null || files.Count <= 5).WithMessage("Maksimum 5 yeni şəkil faylı yükləyə bilərsiniz.")
            .When(x => x.NewImageFiles != null && x.NewImageFiles.Any());

        RuleForEach(x => x.NewImageFiles)
            .NotNull().WithMessage("Yüklənən yeni şəkil faylı boş ola bilməz.")
            .Must(file => file.Length > 0).WithMessage("Yüklənən yeni şəkil faylı boş olmamalıdır.")
            .Must(file => file.Length <= MAX_NEW_IMAGE_FILE_SIZE_MB * 1024 * 1024).WithMessage($"Yüklənən yeni şəkil faylı maksimum {MAX_NEW_IMAGE_FILE_SIZE_MB}MB ola bilər.")
            .Must(file => ALLOWED_IMAGE_TYPES.Contains(file.ContentType)).WithMessage("Yalnız JPG, PNG, GIF və WEBP formatlı şəkillər qəbul olunur.")
            .When(x => x.NewImageFiles != null && x.NewImageFiles.Any());
    }

    private bool HaveUniqueLanguages(List<RoomTranslationUpdateDto> translations)
    {
        if (translations == null || !translations.Any())
        {
            return true;
        }
        return translations.Select(t => t.Language).Distinct().Count() == translations.Count;
    }
}
