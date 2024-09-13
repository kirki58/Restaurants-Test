using Restaurants.Domain.Entitites;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant);
    Task DeleteRestaurantAsync(Restaurant restaurant);
    Task UpdateRestaurantAsync(Restaurant restaurant);
}
