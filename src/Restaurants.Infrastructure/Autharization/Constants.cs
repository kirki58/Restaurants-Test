namespace Restaurants.Infrastructure.Autharization.Constants;

public class AppPolicies
{
    public const string HasNationality = "HasNationality";
    public const string IsTurkish = "IsTurkish";
    public const string OlderThanEighteen = "OlderThanEighteen";
}

public class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string BirthDate = "BirthDate";
}