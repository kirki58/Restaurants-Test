using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Autharization.Constants;

namespace Restaurants.Infrastructure.Autharization;

public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{
    public AppUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
    }

    public async override Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if(user.Nationality != null){
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
        }

        if(user.BirthDate != null){
            id.AddClaim(new Claim(AppClaimTypes.BirthDate, user.BirthDate.Value.ToString("yyyy-MM-dd")));
        }

        id.AddClaim(new Claim(AppClaimTypes.OwnsNRestaurants, user.OwnedRestaurants.Count.ToString()));
        return new ClaimsPrincipal(id);
    }
}
