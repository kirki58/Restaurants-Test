using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Services;

public static class RestaurantAuthorizationService
{
    public static bool IsAuthorized(this UserRecord user, Restaurant restaurant){
        return restaurant.AdminId == user.Id || user.IsInRole("Admin");
    } 
}
