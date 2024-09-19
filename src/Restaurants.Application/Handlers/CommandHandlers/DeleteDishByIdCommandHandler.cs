using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.Services;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class DeleteDishByIdCommandHandler(
    ILogger<DeleteDishByIdCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IUserContext userContext
) : IRequestHandler<DeleteDishByIdCommand>
{
    public async Task Handle(DeleteDishByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Dish with ID: {DishId} from Restaurant with Id: {RestaurantId}", request.DishId, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant == null){
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var user = userContext.GetCurrentUser();
        if(!user.IsAuthorized(restaurant)){
            throw new ForbidException(user.Id, this.GetType().Name, nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if(dish == null){
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        await dishesRepository.DeleteDishByIdAsync(dish);
    }
}
