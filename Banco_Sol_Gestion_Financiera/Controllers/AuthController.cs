using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Banco_Sol_Gestion_Financiera.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;


        public AuthController(IAuthService service)
        {
            _service = service;
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


            return Ok(response);
        }

        [HttpGet("hash")]
        public IActionResult GenerateHash()
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("123456");

            return Ok(hash);
        }
    }
}
