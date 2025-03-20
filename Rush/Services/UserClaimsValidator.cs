using System.Security.Claims;

namespace Rush.WebAPI.Services;

public class UserClaimsValidator
{

    public ClaimsPrincipal User { get; set; }
    
    public UserClaimsValidator(ClaimsPrincipal user)
    {
        User = user;
    }
    
    public string GetRoleName()
    {
        var claims = User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .FirstOrDefault();

        return claims;
    }
    
    public Guid GetUserId()
    {
        var claims = User.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .FirstOrDefault();

        Guid id = Guid.Parse(claims);

        return id;
    }

    public bool CheckForRole(string[] roles)
    {
        string role = GetRoleName();
        
        return roles.Contains(role);
    }
    
    
    
}