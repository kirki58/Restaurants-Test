using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class CreateRestaurantCommandHandler(
    IRestaurantsRepository repository, 
    ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateRestaurantCommand, RestaurantDTO?>
{
    public async Task<RestaurantDTO?> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new restaurant resource: {@Restaurant}", request);
        //Map CreateRestaurantDTO to Restaurant type than pass to repository.
        Restaurant restaurant;
        try{
            restaurant = mapper.Map<Restaurant>(request);
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
