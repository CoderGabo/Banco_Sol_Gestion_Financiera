using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.Income
{
    public interface IIncomeService
    {
        Task<IncomeResponseDto> CreateAsync(CreateIncomeDto dto, int userId);
        Task<IEnumerable<IncomeResponseDto>> GetMyIncomesAsync(int userId);
        Task<IncomeResponseDto?> GetByIdAsync(int id, int userId);
    }
}
