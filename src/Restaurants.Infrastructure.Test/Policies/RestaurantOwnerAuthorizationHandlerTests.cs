using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Infrastructure.Autharization.Constants;
using Restaurants.Infrastructure.Autharization.Requirements;
using Restaurants.Infrastructure.Autharization.Requirements.Handlers;

namespace Restaurants.Infrastructure.Test.Policies;

public class RestaurantOwnerAuthorizationHandlerTests
{
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void HandleRequirement_Has2OrMoreRestaurants_ShouldSucceed(int nRes){
        //
        var requirement = new RestaurantOwnerAuthorization(2);

        var identity = new ClaimsIdentity(new List<Claim>{new Claim(AppClaimTypes.OwnsNRestaurants, nRes.ToString())});
        var userClaimsPrincipal = new ClaimsPrincipal(identity);

        var context = new AuthorizationHandlerContext([requirement], userClaimsPrincipal, null);

        var handler = new RestaurantOwnerAuthorizationHandler();
        //
        handler.HandleAsync(context);

        //
        context.HasSucceeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void HandleRequirement_HasLessThan2Restaurants_ShouldFail(int nRes){
        //
        var requirement = new RestaurantOwnerAuthorization(2);

        var identity = new ClaimsIdentity(new List<Claim>{new Claim(AppClaimTypes.OwnsNRestaurants, nRes.ToString())});
        var userClaimsPrincipal = new ClaimsPrincipal(identity);

        var context = new AuthorizationHandlerContext([requirement], userClaimsPrincipal, null);

        var handler = new RestaurantOwnerAuthorizationHandler();
        //
        handler.HandleAsync(context);

        //
        context.HasSucceeded.Should().BeFalse();
    }
}
