namespace Restaurants.Domain.Entitites;

public record class Address
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
