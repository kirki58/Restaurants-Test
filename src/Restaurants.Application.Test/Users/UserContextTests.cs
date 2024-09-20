using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Xunit.Sdk;

namespace Restaurants.Application.Test.Users;

public class UserContextTests
{
    [Fact]
    public void GetCurrentUser_WithAuthanticatedUser_ReturnsUserRecord(){

        // Arrange
        var httpContextAccessor = new Mock<IHttpContextAccessor>(); // Mock the IHttpContextAccessor dependancy in UserContext so it works without Dependancy injection in test enviorment.

        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, "TestId"),
            new Claim(ClaimTypes.Email, "test@testuser.com"),
            new Claim(ClaimTypes.Role, AppRoles.Admin),
            new Claim(ClaimTypes.Role, AppRoles.User),
            new Claim("Nationality", "Turkish"),
            new Claim("BirthDate", new DateOnly(2000, 2, 2).ToString("yyyy-MM-dd"))
        };

        var identity = new ClaimsIdentity(claims, "TestUserAuth");

        httpContextAccessor.Setup(ca => ca.HttpContext).Returns(new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(identity)
        });

        var userContext = new UserContext(httpContextAccessor.Object);

        // Act
        var currentUser = userContext.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("TestId");
        currentUser.Email.Should().Be("test@testuser.com");
        currentUser.Roles.Should().ContainInOrder(AppRoles.Admin, AppRoles.User);
    }

    [Fact]
    public void GetCurrentUser_WithUnauthanticatedUser_ThrowsInvaidOperationException(){

        // Arrange
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

        httpContextAccessor.Setup(r => r.HttpContext).Returns(new DefaultHttpContext(){
            User = new ClaimsPrincipal(new ClaimsIdentity()) // By default, IsAuthenticated is false for an empty identity
        });

        var userContext = new UserContext(httpContextAccessor.Object);

        //Act
        var action = () => userContext.GetCurrentUser();

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("User not authanticated");
    }

    [Fact]
    public void GetCurrentUser_WithNoHttpContext_ThrowsInvaidOperationException(){

        // Arrange
        var httpContextAccessor = new Mock<IHttpContextAccessor>();

       httpContextAccessor.Setup(ca => ca.HttpContext).Returns((HttpContext) null);

        var userContext = new UserContext(httpContextAccessor.Object);

        //Act
        var action = () => userContext.GetCurrentUser();

        //Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("User not found in the HTTP Context");
    }
}
