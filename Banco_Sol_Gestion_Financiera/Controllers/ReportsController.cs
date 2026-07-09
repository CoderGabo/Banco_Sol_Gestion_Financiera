using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.DTOs;
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

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(
            [FromQuery] BalanceRequestDto request)
        {
            var userId = User.GetUserId();

            var report = await _reportService.GetBalanceAsync(
                userId,
                request);

            return Ok(report);
        }
    }
}
