using System;
using MediatR;

namespace Restaurants.Application.Commands;

public class DeleteRestaurantCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
