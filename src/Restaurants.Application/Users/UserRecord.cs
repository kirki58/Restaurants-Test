namespace Restaurants.Application.Users;

public record class UserRecord(string Id, string Email, IEnumerable<string> Roles)
{
    public bool IsInRole(string role){
        return this.Roles.Contains(role);
    }
}
