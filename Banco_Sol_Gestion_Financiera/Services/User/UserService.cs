using Banco_Sol_Gestion_Financiera.Common;
using Banco_Sol_Gestion_Financiera.Data;
using Banco_Sol_Gestion_Financiera.DTOs;
using Banco_Sol_Gestion_Financiera.Mappers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Banco_Sol_Gestion_Financiera.Services.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreateUserDto> _validator;

        public UserService(
            AppDbContext context,
            IValidator<CreateUserDto> validator
        )
        {
            _context = context;
            _validator = validator;
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            var exists = await _context.Users
                .AnyAsync(x => x.Email == dto.Email);

            if (exists)
                throw new ArgumentException(ErrorMessages.EMAIL_ALREADY_EXISTS);

            var user = new Entities.User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user.ToDto();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(x => x.ToDto());
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return user.ToDto();
        }
    }
}
