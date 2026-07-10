using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.ExchangeRate
{
    public class ExchangeRateCache
    {
        private ExchangeRateDto? _lastRate =
             new ExchangeRateDto
             {
                 BaseCurrency = "USD",
                 TargetCurrency = "BOB",
                 ExchangeRate = 9.925m,
                 Unit = 1,
                 UpdatedAt = DateTime.UtcNow
             };

        public ExchangeRateDto? Get()
        {
            return _lastRate;
        }

        public void Set(ExchangeRateDto rate)
        {
            _lastRate = rate;
        }
    }
}
