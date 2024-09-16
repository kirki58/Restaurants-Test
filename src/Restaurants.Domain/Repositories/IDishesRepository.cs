using System;
using Restaurants.Domain.Entitites;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<Dish> CreateDishAsync(Dish dish);
    Task ClearDishesAsync(IEnumerable<Dish> dishes);
    Task DeleteDishByIdAsync(Dish dish);
}
