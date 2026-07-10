using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Services.Auth;
using Banco_Sol_Gestion_Financiera.Services.ExchangeRate;
using Microsoft.AspNetCore.Mvc;

namespace Banco_Sol_Gestion_Financiera.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<ExchangeRateService> _logger;

        public AuthController(
            IAuthService service,
            ILogger<ExchangeRateService> logger
        )
        {
            _service = service;
            _logger = logger;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response =
                await _service.LoginAsync(dto);


            if (response == null)
                return Unauthorized(
                    new
                    {
                        message = "Credenciales incorrectas"
                    });

            _logger.LogInformation("Login Exitoso!.");
            return Ok(response);
        }

        [HttpGet("hash")]
        public IActionResult GenerateHash()
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("123456");

            _logger.LogInformation("Hash Exitoso!.");
            return Ok(hash);
        }
    }
}
