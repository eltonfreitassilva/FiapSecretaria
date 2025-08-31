using System.Security.Claims;

namespace FIAP.Secretaria.Infrastructure.Security;

public interface IUserAccessor
{
    List<Claim> GetClaims();
    int? GetUserId();
    string? GetUserEmail();
    string? GetUserName();
    string? GetUserRole();
    bool IsAuthenticated();
    bool IsInRole(string role);
}