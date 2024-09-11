using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Services;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync();
    Task<RestaurantDTO?> GetRestaurantAsync(int id);
    Task<RestaurantDTO?> CreateRestaurantAsync(CreateRestaurantDTO createRestaurantDTO);
}
