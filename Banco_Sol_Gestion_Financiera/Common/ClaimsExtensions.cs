using System.Security.Claims;

namespace Banco_Sol_Gestion_Financiera.Common
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException(
                    "Usuario no autenticado."
                );

            return int.Parse(userId);
        }
    }
}
