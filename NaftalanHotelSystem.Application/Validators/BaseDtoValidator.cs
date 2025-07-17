using FluentValidation;
using NaftalanHotelSystem.Application.DataTransferObject.Common;

namespace NaftalanHotelSystem.Application.Validators;

public abstract class BaseDtoValidator<T> : AbstractValidator<T> where T : BaseDto
{
    public BaseDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID sıfırdan böyük olmalıdır.");
    }
}
public abstract class BaseTranslationDtoValidator<T> : AbstractValidator<T> where T : BaseTranslationDto
{
    public BaseTranslationDtoValidator()
    {
        RuleFor(x => x.Language)
            .IsInEnum()
            .WithMessage("Dil dəyəri etibarlı deyil. Yalnız 'Az' (1), 'En' (2) və ya 'Ru' (3) ola bilər.")
            .NotNull().WithMessage("Dil boş ola bilməz.");
    }
}
