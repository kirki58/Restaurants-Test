using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries;

public class GetAllDishesQuery(int restaurantId) : IRequest<IEnumerable<DishDTO>>
{
    public int RestaurantId { get;} = restaurantId;
}
