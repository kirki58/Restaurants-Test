using System;
using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Commands;

public class CreateDishCommand : IRequest
{
    public CreateDishCommand(int restaurantId, CreateDishDTO dto)
    {
        this.Name = dto.Name;
        this.Description = dto.Description;
        this.Price = dto.Price;
        this.Kcal = dto.Kcal;
        this.RestaurantId = restaurantId;
    }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? Kcal { get; set; }
    public int RestaurantId { get; set; }
}
