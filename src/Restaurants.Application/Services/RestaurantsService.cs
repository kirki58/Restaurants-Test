using System;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantsService(IRestaurantsRepository repository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync()
    {
        logger.LogInformation("Fetching All Restaurants From Database");
        var restaurants = await repository.GetAllAsync();
        return restaurants.Select(RestaurantDTO.FromEntity);
    }

    public async Task<RestaurantDTO?> GetRestaurantAsync(int id){
        logger.LogInformation("Fetching Restaurant with ID: " + id);
        var restaurant = await repository.GetRestaurantByIdAsync(id);
        return restaurant == null ? null : RestaurantDTO.FromEntity(restaurant);
    }
}
