using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banco_Sol_Gestion_Financiera.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/exchange-rate")]
    public class ExchangeRateController: ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<ExchangeRateService> _logger;

        public ExchangeRateController(
            IExchangeRateService exchangeRateService,
            ILogger<ExchangeRateService> logger
        )
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rate = await _exchangeRateService.GetCurrentRateAsync();
            _logger.LogInformation("ExchangeRate Exitoso!.");
            return Ok(rate);
        }
    }
}
