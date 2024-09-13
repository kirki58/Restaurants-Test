using System;
using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Commands;

public class CreateRestaurantCommand : IRequest<RestaurantDTO?>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public bool HasDelivery { get; set; }
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public int Tables { get; set; }
}
