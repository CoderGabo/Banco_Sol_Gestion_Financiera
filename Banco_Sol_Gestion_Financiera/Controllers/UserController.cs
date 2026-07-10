using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using Banco_Sol_Gestion_Financiera.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Banco_Sol_Gestion_Financiera.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<ExchangeRateService> _logger;

        public UsersController(
            IUserService userService,
            ILogger<ExchangeRateService> logger
        )
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var user = await _userService.CreateAsync(dto);

            _logger.LogInformation("Crear Usuario Exitoso!.");
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            _logger.LogInformation("Obtener Usuarios Exitoso!.");
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
            {
                _logger.LogInformation("Usuario no encontrado!.");
                return NotFound();
            }

            _logger.LogInformation("Obtener Usuario Exitoso!.");
            return Ok(user);
        }
    }
}
