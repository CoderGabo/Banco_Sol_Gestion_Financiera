using Banco_Sol_Gestion_Financiera.Data;
using Banco_Sol_Gestion_Financiera.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Banco_Sol_Gestion_Financiera.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;


        public AuthService(
            AppDbContext context,
            IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }


        public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email);


            if (user == null)
                return null;


            var passwordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.Password);


            if (!passwordValid)
                return null;


            var token =
                _jwtService.GenerateToken(
                    user.Id,
                    user.Email,
                    user.Name);


            return new TokenResponseDto
            {
                Token = token,

                Expiration =
                    DateTime.UtcNow.AddMinutes(
                        60)
            };
        }
    }
}
