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

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rate = await _exchangeRateService.GetCurrentRateAsync();

            return Ok(rate);
        }
    }
}
