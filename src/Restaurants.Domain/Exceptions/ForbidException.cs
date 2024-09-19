using System;

namespace Restaurants.Domain.Exceptions;

public class ForbidException : Exception
{
    public ForbidException(string userId, string serviceName, string entityName, string entityId) : base($"UserContext with Id: {userId} is Unauthorized to access: {nameof(serviceName)} for entity {nameof(entityName)} with Id: {entityId}")
    {
    }
}
