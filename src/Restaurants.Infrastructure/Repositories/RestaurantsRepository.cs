using System;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<(IEnumerable<Restaurant>, int)> GetAllAsync(int pageSize, int pageNo, string? searchPhrase, int? category)
    {
        var baseQuery = dbContext.Restaurants
            .Where(r => 
                (searchPhrase == null || r.Name.ToLower().Contains(searchPhrase.ToLower()) ||  r.Description.ToLower().Contains(searchPhrase.ToLower()))
                &&
                (category == null || (int) r.Category == category)
            );

        var count = await baseQuery.CountAsync();

        var result = await baseQuery
                    .Skip((pageNo -1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

        return (result, count);
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
