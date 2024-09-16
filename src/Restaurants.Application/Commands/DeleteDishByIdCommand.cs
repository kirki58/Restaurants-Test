using System;
using MediatR;

namespace Restaurants.Application.Commands;

public class DeleteDishByIdCommand(int restaurantId, int dishId) : IRequest
{
    public int RestaurantId { get;} = restaurantId; 
    public int DishId { get;} = dishId;
}
