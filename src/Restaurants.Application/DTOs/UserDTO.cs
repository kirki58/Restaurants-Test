namespace Restaurants.Application.DTOs;

public class UserDTO(string id, string? nationality, string? email){
    public string Id { get; set; } = id;
    public string? Nationality { get; set; } = nationality;
    public string? Email { get; set; } = email;
}
