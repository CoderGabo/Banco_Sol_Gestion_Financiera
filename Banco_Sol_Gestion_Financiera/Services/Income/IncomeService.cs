using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.Data;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Enums;
using Banco_Sol_Gestion_Financiera.Mappers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Banco_Sol_Gestion_Financiera.Services.Income
{
    public class IncomeService: IIncomeService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreateIncomeDto> _validator;

        public IncomeService(
            AppDbContext context,
            IValidator<CreateIncomeDto> validator
        ) { 
            _context = context;
            _validator = validator;
        }
        public async Task<IncomeResponseDto> CreateAsync(CreateIncomeDto dto, int userId)
        {
            await _validator.ValidateAndThrowAsync(dto);
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new KeyNotFoundException(ErrorMessages.USER_NOT_FOUND);


            var currency = Enum.Parse<CurrencyEnum>(dto.Currency, true);

            var income = new Entities.Income
            {
                UserId = userId,
                Amount = dto.Amount,
                Currency = currency,
                Description = dto.Description,
                Source = dto.Source,
                ReceivedAt = DateTime.SpecifyKind(dto.ReceivedAt, DateTimeKind.Unspecified),
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            };

            _context.Incomes.Add(income);

            await _context.SaveChangesAsync();

            return income.ToDto();
        }

        public async Task<IEnumerable<IncomeResponseDto>> GetMyIncomesAsync(int userId)
        {
            var incomes = await _context.Incomes
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return incomes.Select(x => x.ToDto());
        }

        public async Task<IncomeResponseDto?> GetByIdAsync(
            int id,
            int userId)
        {
            var income = await _context.Incomes
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.UserId == userId);

            return income?.ToDto();
        }
    }
}
