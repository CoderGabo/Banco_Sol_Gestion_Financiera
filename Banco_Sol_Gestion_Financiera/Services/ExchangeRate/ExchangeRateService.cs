using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.ExchangeRate
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExchangeRateService> _logger;

        public ExchangeRateService(
            HttpClient httpClient,
            ILogger<ExchangeRateService> logger
        )
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ExchangeRateDto> GetCurrentRateAsync()
        {
            _logger.LogInformation("Consultando tipo de cambio en HexaRate.");
            var response = await _httpClient.GetAsync("rates/USD/BOB/latest");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<HexaRateResponseDto>();
            
            if (result?.Data is null)
                throw new Exception("No fue posible obtener el tipo de cambio.");

            return new ExchangeRateDto
            {
                BaseCurrency = result.Data.Base,
                TargetCurrency = result.Data.Target,
                ExchangeRate = result.Data.Mid,
                Unit = result.Data.Unit,
                UpdatedAt = result.Data.Timestamp
            };
        }
    }
}
