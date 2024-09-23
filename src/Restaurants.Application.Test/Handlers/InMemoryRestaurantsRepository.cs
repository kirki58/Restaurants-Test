using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Test.Handlers.InMemoryRepository;

internal class InMemoryRestaurantsRepository
{
    private readonly List<Restaurant> _restaurants = new();
    internal Restaurant? CreateRestaurantAsync(Restaurant restaurant){
        _restaurants.Add(restaurant);
        return _restaurants.Find(r => r.Id == restaurant.Id);
    }
}
