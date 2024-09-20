using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Restaurants.Domain.Entitites;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? BirthDate { get; set; }
    public string? Nationality { get; set; }

    // Navigation property for owned restaurants
    public List<Restaurant> OwnedRestaurants { get; set; } = [];
}
