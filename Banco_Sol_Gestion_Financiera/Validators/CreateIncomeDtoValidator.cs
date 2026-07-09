using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Enums;
using FluentValidation;

namespace Banco_Sol_Gestion_Financiera.Validators
{
    public class CreateIncomeDtoValidator : AbstractValidator<CreateIncomeDto>
    {
        public CreateIncomeDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.INVALID_AMOUNT);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(ErrorMessages.DESCRIPTION_REQUIRED)
                .MaximumLength(255)
                .WithMessage(ErrorMessages.DESCRIPTION_TOO_LONG);

            RuleFor(x => x.Source)
                .NotEmpty()
                .WithMessage(ErrorMessages.SOURCE_REQUIRED)
                .MaximumLength(50)
                .WithMessage(ErrorMessages.SOURCE_TOO_LONG);

            RuleFor(x => x.ReceivedAt)
                .NotEmpty()
                .WithMessage(ErrorMessages.RECEIVED_DATE_REQUIRED)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(ErrorMessages.RECEIVED_DATE_FUTURE);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Must(x =>
                    Enum.TryParse<CurrencyEnum>(x, true, out _))
                .WithMessage(ErrorMessages.INVALID_CURRENCY);
        }
    }
}
