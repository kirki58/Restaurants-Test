using System;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.DTOs;

public class RestaurantDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public RestaurantCategory Category { get; set; }
    public bool HasDelivery { get; set; }
    public Address? Address { get; set; }
    public List<DishDTO> Dishes { get; set; } = [];
    public int Tables { get; set; }

    public static RestaurantDTO FromEntity(Restaurant restaurant){
        return new RestaurantDTO{
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            Category = restaurant.Category,
            HasDelivery = restaurant.HasDelivery,
            Address = restaurant.Address,
            Dishes = restaurant.Dishes.Select(DishDTO.FromEntity).ToList(),
            Tables = restaurant.Tables
        };
    }
}
