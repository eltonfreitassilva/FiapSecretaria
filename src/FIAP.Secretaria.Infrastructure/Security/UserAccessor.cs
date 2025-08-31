using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FIAP.Secretaria.Infrastructure.Security;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<Claim> GetClaims()
    {
        var claims = new List<Claim>();

        if (_httpContextAccessor?.HttpContext?.User != null)
        {
            var claimsUser = _httpContextAccessor
                .HttpContext
                .User?
                .Claims?
                .ToList();

            if (claimsUser != null)
                claims.AddRange(claimsUser);
        }

        return claims;
    }

    public int? GetUserId()
    {
        var userIdClaim = GetClaim(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            return userId;
        return null;
    }

    public string? GetUserEmail()
    {
        return GetClaim(ClaimTypes.Email)?.Value;
    }

    public string? GetUserName()
    {
        return GetClaim(ClaimTypes.Name)?.Value;
    }

    public string? GetUserRole()
    {
        return GetClaim(ClaimTypes.Role)?.Value;
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public bool IsInRole(string role)
    {
        return _httpContextAccessor?.HttpContext?.User?.IsInRole(role) ?? false;
    }

    private Claim? GetClaim(string claimType)
    {
        return _httpContextAccessor?.HttpContext?.User?
            .Claims?
            .FirstOrDefault(c => c.Type == claimType);
    }
}