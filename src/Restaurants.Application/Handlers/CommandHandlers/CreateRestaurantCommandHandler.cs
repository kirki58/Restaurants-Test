using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class CreateRestaurantCommandHandler(
    IRestaurantsRepository repository, 
    ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IUserContext userContext
) : IRequestHandler<CreateRestaurantCommand, RestaurantDTO>
{
    public async Task<RestaurantDTO> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        //Map CreateRestaurantDTO to Restaurant type than pass to repository.
        Restaurant restaurant;
        try{
            restaurant = mapper.Map<Restaurant>(request);
        }
        catch{
            throw new ConflictException("Failed to map CreateRestaurantCommand to Restaurant");
        }
        var currentUser = userContext.GetCurrentUser();
        restaurant.AdminId = currentUser.Id;

        logger.LogInformation("User: {@User} Creating new restaurant resource: {@Restaurant}", currentUser ,request);
        var newRes = await repository.CreateRestaurantAsync(restaurant); // Throws an internal exception on failure.

        //Map to RestaurantDTO and return
        var restaurantDTO = mapper.Map<RestaurantDTO>(newRes);
        return restaurantDTO;
    }
}