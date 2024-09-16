using System;
using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries;

public class GetDishByIdQuery(int restaurantId, int dishId) : IRequest<DishDTO>
{
    public int RestaurantId { get;} = restaurantId;
    public int DishId { get;} = dishId;
}
