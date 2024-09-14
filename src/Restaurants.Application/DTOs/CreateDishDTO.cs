using System;

namespace Restaurants.Application.DTOs;

public class CreateDishDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int? Kcal { get; set; }
}
