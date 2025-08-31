using System.Security.Claims;

namespace FIAP.Secretaria.Infrastructure.Security;

public class EmptyUserAccessor : IUserAccessor
{
    public List<Claim> GetClaims() => new List<Claim>();
    public int? GetUserId() => null;
    public string? GetUserEmail() => null;
    public string? GetUserName() => null;
    public string? GetUserRole() => null;
    public bool IsAuthenticated() => false;
    public bool IsInRole(string role) => false;
}