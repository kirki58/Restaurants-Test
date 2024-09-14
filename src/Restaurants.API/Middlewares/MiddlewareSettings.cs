using System;

namespace Restaurants.API.Middlewares;

public class MiddlewareSettings
{
    public double RequestTimeoutLimit { get; set; } // RegisterTimeout middleware timeout limit in seconds
}
