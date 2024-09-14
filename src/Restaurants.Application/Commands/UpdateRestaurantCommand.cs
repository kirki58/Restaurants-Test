using System;
using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Commands;

public class UpdateRestaurantCommand : IRequest
{
    public UpdateRestaurantCommand(int id, UpdateRestaurantDTO dto)
    {
        this.Id = id;

        this.Name = dto.Name;
        this.Description = dto.Description;
        this.HasDelivery = dto.HasDelivery;
    }

    public int Id { get;}
    public string? Name { get; }
    public string? Description { get; }
    public bool? HasDelivery { get;  }
}
