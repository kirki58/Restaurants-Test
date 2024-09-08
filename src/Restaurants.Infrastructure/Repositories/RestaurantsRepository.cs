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
        .FirstOrDefaultAsync(res => res.Id == id );
    }
}
