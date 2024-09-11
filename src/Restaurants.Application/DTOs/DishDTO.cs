using System;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.DTOs;

public class DishDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int? Kcal { get; set; }
}
