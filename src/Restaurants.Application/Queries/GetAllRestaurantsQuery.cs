using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries;

public class GetAllRestaurantsQuery(string? searchParams, int? restaurantCategory) : IRequest<IEnumerable<RestaurantDTO>>
{
    public string? SearchParams { get; set; } = searchParams;
    public int? Category { get; set; } = restaurantCategory; 
}
