namespace Restaurants.Application.Users;

public interface IUserContext
{
    UserRecord GetCurrentUser();
}
