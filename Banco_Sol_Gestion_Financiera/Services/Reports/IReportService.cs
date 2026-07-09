using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.Reports
{
    public interface IReportService
    {
        Task<BalanceResponseDto> GetBalanceAsync(
            int userId,
            BalanceRequestDto request
        );
    }
}
