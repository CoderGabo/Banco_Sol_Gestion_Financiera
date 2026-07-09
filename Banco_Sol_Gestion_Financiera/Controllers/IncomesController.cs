using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Services.Income;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banco_Sol_Gestion_Financiera.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/incomes")]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncomeDto dto)
        {
            var userId = User.GetUserId();

            var income = await _incomeService.CreateAsync(
                dto,
                userId
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = income.Id },
                income
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();

            var incomes =
                await _incomeService.GetMyIncomesAsync(userId);

            return Ok(incomes);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.GetUserId();

            var income =
                await _incomeService.GetByIdAsync(
                    id,
                    userId
                );

            if (income == null)
                throw new KeyNotFoundException(
                    $"INC003 - No existe el ingreso con id {id}."
                );

            return Ok(income);
        }
    }
}
