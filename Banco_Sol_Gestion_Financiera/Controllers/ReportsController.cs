using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using Banco_Sol_Gestion_Financiera.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banco_Sol_Gestion_Financiera.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ExchangeRateService> _logger;

        public ReportsController(
            IReportService reportService,
            ILogger<ExchangeRateService> logger
        )
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(
            [FromQuery] BalanceRequestDto request)
        {
            var userId = User.GetUserId();

            var report = await _reportService.GetBalanceAsync(
                userId,
                request);

            _logger.LogInformation("Balance Generado Exitosamente!.");
            return Ok(report);
        }
    }
}
