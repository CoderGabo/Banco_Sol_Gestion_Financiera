using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.DTOs;
using FluentValidation;

namespace Banco_Sol_Gestion_Financiera.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ErrorMessages.NAME_REQUIRED)
                .MaximumLength(100)
                .WithMessage(ErrorMessages.NAME_TOO_LONG);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ErrorMessages.EMAIL_REQUIRED)
                .EmailAddress()
                .WithMessage(ErrorMessages.INVALID_EMAIL);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.PASSWORD_REQUIRED)
                .MinimumLength(8)
                .WithMessage(ErrorMessages.PASSWORD_TOO_SHORT);
        }
    }
}
