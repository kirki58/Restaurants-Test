using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Application.Handlers;
using Restaurants.Application.Profiles;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Test.Handlers.CommandHandlers;

public class UpdateRestaurantCommandHandlerTests
{
    [Fact]
    public async Task Handle_UserIsAuthorizedAndRestaurantFound_UpdatesRestaurant(){
        //
        var requestDto = new UpdateRestaurantDTO{
            Name = "updated",
            Description = "updated",
            HasDelivery = false
        };
        var request = new UpdateRestaurantCommand(1, requestDto);

        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        
        var userContextMock = new Mock<IUserContext>();
        var currentUser = new UserRecord("test-user", "test@testuser.com", [AppRoles.Admin]);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var repositoryMock = new Mock<IRestaurantsRepository>();
        var returnedRestaurant = new Restaurant{
            Id = 1,
            Name = "name",
            Description = "desc",
            HasDelivery = true,
            AdminId = currentUser.Id
        };
        repositoryMock.Setup(r => r.GetRestaurantByIdAsync(returnedRestaurant.Id)).ReturnsAsync(returnedRestaurant);

        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<RestaurantProfile>();
        });
        IMapper mapper = config.CreateMapper();

        var shouldMapTo = new Restaurant{
            Id = returnedRestaurant.Id,
            Name = returnedRestaurant.Name,
            Description = returnedRestaurant.Description,
            HasDelivery = returnedRestaurant.HasDelivery,
            AdminId = returnedRestaurant.AdminId
        };
        mapper.Map(request, shouldMapTo); // Mock the mapping

        var handler = new UpdateRestaurantCommandHandler(loggerMock.Object, repositoryMock.Object, mapper, userContextMock.Object);

        //
        await handler.Handle(request, CancellationToken.None);

        //
        repositoryMock.Verify(r => r.UpdateRestaurantAsync(It.Is<Restaurant>(r =>
            r.Id == shouldMapTo.Id &&
            r.Name == shouldMapTo.Name &&
            r.Description == shouldMapTo.Description &&
            r.HasDelivery == shouldMapTo.HasDelivery &&
            r.AdminId == shouldMapTo.AdminId
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNoRestaurantFound_ShouldThrowNotFoundException(){
        //
        var requestDto = new UpdateRestaurantDTO{
            Name = "updated",
            Description = "updated",
            HasDelivery = false
        };
        var request = new UpdateRestaurantCommand(1, requestDto);

        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        
        var userContextMock = new Mock<IUserContext>();

        var repositoryMock = new Mock<IRestaurantsRepository>();
        repositoryMock.Setup(r => r.GetRestaurantByIdAsync(It.IsAny<int>())).ReturnsAsync((Restaurant?) null);

        var mapperMock = new Mock<IMapper>();

        var handler = new UpdateRestaurantCommandHandler(loggerMock.Object, repositoryMock.Object, mapperMock.Object, userContextMock.Object);

        //
        var action = () => handler.Handle(request, CancellationToken.None);

        //
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_UserNotRestaurantAdminOrAppAdmin_ShouldThrowForbidException(){
        //
        var requestDto = new UpdateRestaurantDTO{
            Name = "updated",
            Description = "updated",
            HasDelivery = false
        };
        var request = new UpdateRestaurantCommand(1, requestDto);

        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        
        var userContextMock = new Mock<IUserContext>();
        var currentUser = new UserRecord("test-user", "test@testuser.com", []);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var repositoryMock = new Mock<IRestaurantsRepository>();
        var returnedRestaurant = new Restaurant{
            Id = 1,
            Name = "name",
            Description = "desc",
            HasDelivery = true,
        };
        repositoryMock.Setup(r => r.GetRestaurantByIdAsync(returnedRestaurant.Id)).ReturnsAsync(returnedRestaurant);

        var mapperMock = new Mock<IMapper>();

        var handler = new UpdateRestaurantCommandHandler(loggerMock.Object, repositoryMock.Object, mapperMock.Object, userContextMock.Object);

        //
        var action = () => handler.Handle(request, CancellationToken.None);

        //
        await action.Should().ThrowAsync<ForbidException>();

    }
}
