using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.Contact;

namespace NaftalanHotelSystem.Application.Validators;

public abstract class ContactBaseDtoValidator<T> : AbstractValidator<T> where T : ContactBaseDto
{
    public ContactBaseDtoValidator()
    {
        RuleFor(x => x.Number)
            .NotNull().WithMessage("Nömrələr siyahısı boş ola bilməz.")
            .NotEmpty().WithMessage("Ən azı bir nömrə daxil edilməlidir.");
        RuleForEach(x => x.Number)
            .NotEmpty().WithMessage("Nömrə boş ola bilməz.")
            .Matches(@"^\+?\d{7,15}$").WithMessage("Nömrə etibarlı telefon formatında olmalıdır (məsələn, +994501234567).");

        RuleFor(x => x.Mail)
            .NotEmpty().WithMessage("Mail boş ola bilməz.")
            .EmailAddress().WithMessage("Mail etibarlı email formatında olmalıdır.");

        RuleFor(x => x.Adress)
            .NotEmpty().WithMessage("Ünvan boş ola bilməz.")
            .MaximumLength(500).WithMessage("Ünvan maksimum 500 simvol ola bilər.");

        RuleFor(x => x.InstagramLink)
            .Matches(@"^(https?:\/\/)?(www\.)?instagram\.com\/([a-zA-Z0-9_.]+)$").WithMessage("Instagram linki etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.InstagramLink));

        RuleFor(x => x.FacebookLink)
            .Matches(@"^(https?:\/\/)?(www\.)?facebook\.com\/[a-zA-Z0-9.]+$").WithMessage("Facebook linki etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.FacebookLink));

        RuleFor(x => x.TiktokLink)
            .Matches(@"^(https?:\/\/)?(www\.)?tiktok\.com\/@([a-zA-Z0-9_.]+)$").WithMessage("TikTok linki etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.TiktokLink));

        RuleFor(x => x.YoutubeLink)
            .Matches(@"^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/.+$").WithMessage("YouTube linki etibarlı formatda olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.YoutubeLink));

        RuleFor(x => x.WhatsappNumber)
            .Matches(@"^\+?\d{7,15}$").WithMessage("Whatsapp nömrəsi etibarlı telefon formatında olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.WhatsappNumber));
    }
}
public class ContactDtoValidator : ContactBaseDtoValidator<ContactDto>
{
    public ContactDtoValidator()
    {
       
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");
    }
}
public class ContactUpdateDtoValidator : ContactBaseDtoValidator<ContactUpdateDto>
{
    public ContactUpdateDtoValidator()
    {
        
    }
}
