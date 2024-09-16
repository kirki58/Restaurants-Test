using System;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<Dish> CreateDishAsync(Dish dish)
    {
        await dbContext.Dishes.AddAsync(dish);
        await dbContext.SaveChangesAsync();
        return dish;
    }

    public async Task ClearDishesAsync(IEnumerable<Dish> dishes)
    {
        dbContext.RemoveRange(dishes);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteDishByIdAsync(Dish dish)
    {
        dbContext.Remove(dish);
        await dbContext.SaveChangesAsync();
    }
}
