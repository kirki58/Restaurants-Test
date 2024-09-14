using System;
using Restaurants.Domain.Entitites;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task CreateDishAsync(Dish dish);
}
