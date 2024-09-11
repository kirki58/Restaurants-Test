using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantsService(
    IRestaurantsRepository repository, 
    ILogger<RestaurantsService> logger,
    IMapper mapper
    ) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDTO>> GetRestaurantsAsync()
    {
        logger.LogInformation("Fetching All Restaurants From Database");
        var restaurants = await repository.GetAllAsync();
        // Map Restaurant type into RestaurantDTO
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        return restaurantDtos;
    }

    public async Task<RestaurantDTO?> GetRestaurantAsync(int id){
        logger.LogInformation("Fetching Restaurant with ID: " + id);
        var restaurant = await repository.GetRestaurantByIdAsync(id);
        // Map Restaurant type into RestaurantDTO type
        var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);
        return  restaurantDTO;
    }

    public async Task<RestaurantDTO?> CreateRestaurantAsync(CreateRestaurantDTO createRestaurantDTO)
    {
        logger.LogInformation("Creating new restaurant resource...");
        //Map CreateRestaurantDTO to Restaurant type than pass to repository.
        Restaurant restaurant;
        try{
            restaurant = mapper.Map<Restaurant>(createRestaurantDTO);
        }
        catch{
            logger.LogError("Failed to map CreateRestaurantDTO to Restaurant");
            return null;
        }

    
        var newRes = await repository.CreateRestaurantAsync(restaurant);

        //Map to RestaurantDTO and return
        try{
            var restaurantDTO = mapper.Map<RestaurantDTO>(newRes);
            return restaurantDTO;
        }
        catch{
            logger.LogError("Failed to map Restaurant to RestaurantDTO");
            return null;
        }
    }
}
