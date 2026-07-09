using Banco_Sol_Gestion_Financiera.DTOs;

namespace Banco_Sol_Gestion_Financiera.Services.User
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(CreateUserDto dto);

        Task<IEnumerable<UserResponseDto>> GetAllAsync();

        Task<UserResponseDto?> GetByIdAsync(int id);
    }
}
