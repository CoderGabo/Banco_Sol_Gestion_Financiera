using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.ExchangeRate
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateDto> GetCurrentRateAsync();
    }
}
