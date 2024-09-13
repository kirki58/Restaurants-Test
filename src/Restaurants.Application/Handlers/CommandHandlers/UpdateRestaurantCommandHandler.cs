using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
 IRestaurantsRepository repository,
 IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Restaurant entity with ID: {RestaurantID} with values {@NewValues}", request.Id, request);
        var restaurant = await repository.GetRestaurantByIdAsync(request.Id);

        if(restaurant == null){
            logger.LogWarning($"Restaurant with ID {request.Id} not found");
            return false;
        }

        mapper.Map(request, restaurant);

        await repository.UpdateRestaurantAsync(restaurant);
        return true;
    }
}
