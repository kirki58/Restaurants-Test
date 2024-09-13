using System;
using System.Reflection.Metadata.Ecma335;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository repository)
    : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Trying to delete item with ID: {request.Id}");
        var restaurant = await repository.GetRestaurantByIdAsync(request.Id);

        if(restaurant == null)
        {
            logger.LogWarning($"Restaurant with ID {request.Id} not found");
            return false;
        }

        await repository.DeleteRestaurantAsync(restaurant);
        return true;
    }
}
