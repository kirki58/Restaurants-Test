using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Extensions;

public static class AppCollectionExtensions
{
    public static async Task UseInfrastructureAsync(this WebApplication app){
        // SEED DATA
        await using (var scope = app.Services.CreateAsyncScope()){
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<RestaurantsDbContext>();
            await RestaurantsDbContext.SeedData(context);
        }
    }
}
