using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Application.Handlers;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Test.Handlers.CommandHandlers;

public class CreateRestaurantCommandHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsDTO_OnSuccess(){
        //
        var currentUser = new UserRecord("admin-id", "test@testadmin.com", []);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var repositoryMock = new Mock<IRestaurantsRepository>();
        repositoryMock.Setup(r => r.CreateRestaurantAsync(It.IsAny<Restaurant>()))!.ReturnsAsync((Restaurant restaurant) => restaurant);

        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Restaurant>(It.IsAny<CreateRestaurantCommand>())).Returns(new Restaurant{
            Id = 1,
            Name = "Name",
            Category = RestaurantCategory.Turkish,
            Description = "desc",
            HasDelivery = true,
        });

        mapperMock.Setup(m => m.Map<RestaurantDTO>(It.IsAny<Restaurant>())).Returns((Restaurant r) => new RestaurantDTO{
            Id = r.Id,
            Name = r.Name,
            Category = r.Category,
            Description = r.Description,
            HasDelivery = r.HasDelivery,
            RestaurantAdmin = new UserDTO(r.AdminId, "not-significant", "not-significant")
        });

        var handler = new CreateRestaurantCommandHandler(repositoryMock.Object, loggerMock.Object, mapperMock.Object, userContextMock.Object);

        var command = new CreateRestaurantCommand{
            Name = "Name",
            Category = "Turkish",
            Description = "desc",
            HasDelivery = true,
        };
        //
        var result = await handler.Handle(command, CancellationToken.None);

        //
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Name");
        result.Category.Should().Be(RestaurantCategory.Turkish);
        result.Description.Should().Be("desc");
        result.HasDelivery.Should().Be(true);
        
        // Most importantly
        result.RestaurantAdmin.Should().NotBeNull();
        result.RestaurantAdmin.Id.Should().Be(currentUser.Id);
    }
}
