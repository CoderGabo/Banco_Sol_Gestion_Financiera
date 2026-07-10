using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.ExchangeRate
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly ExchangeRateCache _cache;

        public ExchangeRateService(
            HttpClient httpClient,
            ILogger<ExchangeRateService> logger,
            ExchangeRateCache cache
        )
        {
            _httpClient = httpClient;
            _logger = logger;
            _cache = cache;
        }

        public async Task<ExchangeRateDto> GetCurrentRateAsync()
        {
            try
            {
                _logger.LogInformation(
                    "Consultando tipo de cambio en HexaRate."
                );

                var response = await _httpClient.GetAsync(
                    "rates/USD/BOB/latest"
                );


                response.EnsureSuccessStatusCode();


                var result =
                    await response.Content
                    .ReadFromJsonAsync<HexaRateResponseDto>();


                if (result?.Data is null)
                    throw new Exception(
                        "Respuesta invalida desde HexaRate."
                    );


                var exchangeRate = new ExchangeRateDto
                {
                    BaseCurrency = result.Data.Base,
                    TargetCurrency = result.Data.Target,
                    ExchangeRate = result.Data.Mid,
                    Unit = result.Data.Unit,
                    UpdatedAt = result.Data.Timestamp
                };


                // Guardamos ultimo cambio exitoso
                _cache.Set(exchangeRate);


                _logger.LogInformation(
                    "Tipo de cambio obtenido correctamente."
                );


                return exchangeRate;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error consultando HexaRate."
                );


                var cachedRate = _cache.Get();


                if (cachedRate != null)
                {
                    _logger.LogWarning(
                        "Utilizando ultimo tipo de cambio almacenado."
                    );

                    return cachedRate;
                }


                throw new Exception(
                    "No fue posible obtener el tipo de cambio y no existe cache disponible."
                );
            }
        }
    }
}
