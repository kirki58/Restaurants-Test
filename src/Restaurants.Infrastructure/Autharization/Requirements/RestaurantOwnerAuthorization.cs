using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Autharization.Requirements;

public class RestaurantOwnerAuthorization(int minRestaurants) : IAuthorizationRequirement
{
    public int MinRestaurants { get;} = minRestaurants;
}
