using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.Services;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class DeleteDishesCommandHandler(
    ILogger<DeleteDishesCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IUserContext userContext
) : IRequestHandler<DeleteDishesCommand>
{
    public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting all dishes from restaurant with ID: {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant == null){
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        var user = userContext.GetCurrentUser();
        if(!user.IsAuthorized(restaurant)){
            throw new ForbidException(user.Id, nameof(this.GetType), nameof(Restaurant), request.RestaurantId.ToString());
        }

        await dishesRepository.ClearDishesAsync(restaurant.Dishes);
    }
}
