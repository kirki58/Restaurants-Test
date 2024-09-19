namespace Restaurants.Infrastructure.Autharization.Constants;

public static class AppPolicies
{
    public const string HasNationality = "HasNationality";
    public const string IsTurkish = "IsTurkish";
    public const string OlderThanEighteen = "OlderThanEighteen";
    public const string OwnsTwoOrMoreRestaurants = "OwnsTwoOrMoreRestaurants";
}

public static class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string BirthDate = "BirthDate";
    public const string OwnsNRestaurants = "OwnsNRestaurants";
}