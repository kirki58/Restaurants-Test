using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Profiles;
using Restaurants.Application.Services;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services){
        services.AddScoped<IRestaurantsService, RestaurantsService>();
        services.AddAutoMapper(typeof(RestaurantProfile).Assembly);
    }
}
