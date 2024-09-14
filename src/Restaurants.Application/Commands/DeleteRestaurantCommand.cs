using System;
using MediatR;

namespace Restaurants.Application.Commands;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
