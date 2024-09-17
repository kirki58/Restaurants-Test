using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{
    public UserRecord GetCurrentUser(){
        var user = accessor?.HttpContext?.User;
        if(user == null){
            throw new InvalidOperationException("User not found in the HTTP Context");
        }
        if(user.Identity == null || !user.Identity.IsAuthenticated){
            throw new InvalidOperationException("User not authanticated"); // "Authorize" attribute already checks this
        }

        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var rolesClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value);

        if(idClaim == null || emailClaim == null){
            throw new InvalidOperationException("User doesn't have required claim(s)");
        }
        return new UserRecord(idClaim, emailClaim, rolesClaims);
    }
}
