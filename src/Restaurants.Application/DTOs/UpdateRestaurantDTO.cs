using System;

namespace Restaurants.Application.DTOs;

public class UpdateRestaurantDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? HasDelivery { get; set; }
}
