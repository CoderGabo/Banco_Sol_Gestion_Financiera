using Banco_Sol_Gestion_Financiera.Data;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Enums;
using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Banco_Sol_Gestion_Financiera.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IValidator<BalanceRequestDto> _validator;

        public ReportService(
            AppDbContext context,
            IExchangeRateService exchangeRateService,
            IValidator<BalanceRequestDto> validator)
        {
            _context = context;
            _exchangeRateService = exchangeRateService;
            _validator = validator;
        }

        public async Task<BalanceResponseDto> GetBalanceAsync(
            int userId,
            BalanceRequestDto request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var start = request.StartDate.Date;
            var end = request.EndDate.Date.AddDays(1);
            var incomes = await _context.Incomes
                .Where(i =>
                    i.UserId == userId &&
                    i.ReceivedAt >= start &&
                    i.ReceivedAt < end)
                .ToListAsync();

            var exchangeRate = await _exchangeRateService.GetCurrentRateAsync();

            decimal rate = exchangeRate.ExchangeRate;

            decimal total = 0;

            foreach (var income in incomes)
            {
                if (income.Currency == request.Currency)
                {
                    total += income.Amount;
                }
                else
                {
                    if (request.Currency == CurrencyEnum.BOB)
                    {
                        total += income.Amount * rate;
                    }
                    else
                    {
                        total += income.Amount / rate;
                    }
                }
            }

            total = Math.Round(total, 2);

            return new BalanceResponseDto
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Currency = request.Currency,
                Total = total
            };
        }
    }
}
