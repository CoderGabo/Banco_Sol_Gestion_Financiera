using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.DTOs;
using FluentValidation;

namespace Banco_Sol_Gestion_Financiera.Validators
{
    public class BalanceRequestDtoValidator : AbstractValidator<BalanceRequestDto>
    {
        public BalanceRequestDtoValidator()
        {
            RuleFor(x => x.StartDate)
               .NotEmpty()
               .WithMessage(ErrorMessages.START_DATE_REQUIRED)
               .LessThanOrEqualTo(DateTime.Today)
               .WithMessage(ErrorMessages.FUTURE_DATE_NOT_ALLOWED);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(ErrorMessages.END_DATE_REQUIRED)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage(ErrorMessages.FUTURE_DATE_NOT_ALLOWED);

            RuleFor(x => x.Currency)
                .IsInEnum()
                .WithMessage(ErrorMessages.INVALID_CURRENCY);

            RuleFor(x => x)
                .Must(x => x.StartDate <= x.EndDate)
                .WithMessage(ErrorMessages.INVALID_DATE_RANGE);
        }
    }
}
