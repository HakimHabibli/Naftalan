using FluentValidation;

namespace NaftalanHotelSystem.Application.DataTransferObject.Login;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
      
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Düzgün email formatı daxil edin.");

       
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.");
    }
}