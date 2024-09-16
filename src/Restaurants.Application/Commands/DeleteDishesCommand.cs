using System;
using MediatR;

namespace Restaurants.Application.Commands;

public class DeleteDishesCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get;} = restaurantId;
}
