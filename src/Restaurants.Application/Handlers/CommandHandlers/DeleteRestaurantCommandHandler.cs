using System;
using System.Reflection.Metadata.Ecma335;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.Services;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository repository,
    IUserContext userContext)
    : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Trying to delete item with ID: {RestaurantID}", request.Id);
        var restaurant = await repository.GetRestaurantByIdAsync(request.Id);

        if(restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        var user = userContext.GetCurrentUser();
        if(!user.IsAuthorized(restaurant)){
            throw new ForbidException(user.Id, this.GetType().Name, nameof(Restaurant), request.Id.ToString());
        }
        await repository.DeleteRestaurantAsync(restaurant);
    }
}
