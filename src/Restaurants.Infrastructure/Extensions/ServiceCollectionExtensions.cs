using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfraStructure(this IServiceCollection services){
        services.AddDbContext<RestaurantsDbContext>( options => 
            options
                .UseSqlServer(Environment.GetEnvironmentVariable("RestaurantsDb"))
                .EnableSensitiveDataLogging()
        );

        // Add AspNetCore.Identity.EntityFrameworkCore API User Endpoints bound to repositories of RestaurantsDbContext
        services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
    }
}
