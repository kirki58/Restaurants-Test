using System;
using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDTO>
{
    public int Id { get; } = id;
}
