using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class CreateDishCommandHandler(
ILogger<CreateDishCommandHandler> logger, 
IMapper mapper,
IRestaurantsRepository restaurantsRepository,
IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating Dish entity from command: {@Dish}", request);

        var restaurant = restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant == null){
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        Dish dish;

        try{
            dish = mapper.Map<Dish>(request);
        }
        catch{
            throw new ConflictException("Failed to map CreateDishCommand to Restaurant");
        }

        await dishesRepository.CreateDishAsync(dish);
    }
}
