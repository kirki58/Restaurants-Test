using System;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
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
