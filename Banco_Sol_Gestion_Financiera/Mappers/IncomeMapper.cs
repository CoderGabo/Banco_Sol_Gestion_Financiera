using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Entities;

namespace Banco_Sol_Gestion_Financiera.Mappers
{
    public static class IncomeMapper
    {
        public static IncomeResponseDto ToDto(this Income income)
        {
            return new IncomeResponseDto
            {
                Id = income.Id,
                UserId = income.UserId,
                Amount = income.Amount,
                Currency = income.Currency,
                Description = income.Description,
                Source = income.Source,
                ReceivedAt = income.ReceivedAt,
                CreatedAt = income.CreatedAt
            };
        }
    }
}
