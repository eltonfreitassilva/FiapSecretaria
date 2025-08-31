using System.Security.Claims;

namespace FIAP.Secretaria.Infrastructure.Services;

public interface ITokenService
{
    string GenerateToken(int userId, string email, string role);
    ClaimsPrincipal? ValidateToken(string token);
    string GenerateRefreshToken();
}