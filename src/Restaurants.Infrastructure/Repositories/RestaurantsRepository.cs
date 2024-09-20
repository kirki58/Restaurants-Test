using System;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync(string? searchParams, int? category)
    { 
        return await dbContext.Restaurants
            .Where(r => 
                (searchParams == null || r.Name.ToLower().Contains(searchParams.ToLower()) ||  r.Description.ToLower().Contains(searchParams.ToLower()))
                &&
                (category == null || (int) r.Category == category)
            )
            .ToListAsync();
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int id){
        return await 
        dbContext.Restaurants
        .Include(r => r.Dishes)
        .Include(r => r.RestaurantAdmin)
        .FirstOrDefaultAsync(res => res.Id == id );
    }
    public async Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant){
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant;
    }
    public async Task DeleteRestaurantAsync(Restaurant restaurant){
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateRestaurantAsync(Restaurant restaurant){
        dbContext.Restaurants.Update(restaurant);
        await dbContext.SaveChangesAsync();
    }
}
