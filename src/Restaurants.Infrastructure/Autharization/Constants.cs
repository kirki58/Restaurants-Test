namespace Restaurants.Infrastructure.Autharization.Constants;

public static class AppPolicies
{
    public const string HasNationality = "HasNationality";
    public const string IsTurkish = "IsTurkish";
    public const string OlderThanEighteen = "OlderThanEighteen";
}

public static class AppClaimTypes
{
    public const string Id = "Id";
    public const string Nationality = "Nationality";
    public const string BirthDate = "BirthDate";
}