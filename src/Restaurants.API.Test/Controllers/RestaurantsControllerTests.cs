using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Restaurants.API.Test.Controllers;

// This class provides Integration testing rather than Unit testing!
public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllAsync_ForValidRequest_Returns200Ok(){
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/restaurants?pageNo=1&pageSize=5");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAllAsync_ForInvalidRequest_Returns500Internal(){
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/restaurants");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
