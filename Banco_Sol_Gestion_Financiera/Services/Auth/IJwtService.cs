namespace Banco_Sol_Gestion_Financiera.Services.Auth
{
    public interface IJwtService
    {
        string GenerateToken(
            int userId,
            string email,
            string name);
    }
}
