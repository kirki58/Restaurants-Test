using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Infrastructure.Autharization.Constants;

namespace Restaurants.Infrastructure.Autharization.Requirements.Handlers;

public class RestaurantOwnerAuthorizationHandler(IUserContext userContext) : AuthorizationHandler<RestaurantOwnerAuthorization>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantOwnerAuthorization requirement)
    {
        var restaurantsCount = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.OwnsNRestaurants)!.Value);

        if(restaurantsCount >= requirement.MinRestaurants){
            context.Succeed(requirement);
        }
        else{
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
