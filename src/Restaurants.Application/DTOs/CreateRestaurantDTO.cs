using System;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.DTOs;

public class CreateRestaurantDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public RestaurantCategory Category { get; set; }
    public bool HasDelivery { get; set; }
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public int Tables { get; set; }
}
