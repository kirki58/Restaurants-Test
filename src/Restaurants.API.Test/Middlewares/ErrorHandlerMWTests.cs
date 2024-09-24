using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Test.Middlewares;

public class ErrorHandlerMWTests
{
    [Fact]
    public async Task InvokeAsync_ResponseDoesntChange_WhenNoExceptionsCaught(){
        //
        var context = new DefaultHttpContext();
        context.Response.StatusCode = 200;

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<ErrorHandlerMw>>();
        var middleware = new ErrorHandlerMw(logger.Object);

        //
        await middleware.InvokeAsync(context, next.Object);

        //
        context.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task InvokeAsync_StatusCode500_GeneralExceptionCaught(){
        //
        var context = new DefaultHttpContext();
        context.Response.StatusCode = 200;

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(It.IsAny<HttpContext>())).ThrowsAsync(new Exception("Simulation exception"));

        var logger = new Mock<ILogger<ErrorHandlerMw>>();
        var middleware = new ErrorHandlerMw(logger.Object);

        //
        await middleware.InvokeAsync(context, next.Object);

        //
        context.Response.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task InvokeAsync_StatusCode409_ConflictExceptionCaught(){
        //
        var context = new DefaultHttpContext();
        context.Response.StatusCode = 200;

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(It.IsAny<HttpContext>())).ThrowsAsync(new ConflictException("Conflict Exception thrown in the pipeline"));

        var logger = new Mock<ILogger<ErrorHandlerMw>>();
        var middleware = new ErrorHandlerMw(logger.Object);

        //
        await middleware.InvokeAsync(context, next.Object);

        //
        context.Response.StatusCode.Should().Be(409);
    }

    [Fact]
    public async Task InvokeAsync_StatusCode404_NotFoundExceptionCaught(){
        //
        var context = new DefaultHttpContext();
        context.Response.StatusCode = 200;

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(It.IsAny<HttpContext>())).ThrowsAsync(new NotFoundException(nameof(Restaurant), "SimulationRestaurant"));

        var logger = new Mock<ILogger<ErrorHandlerMw>>();
        var middleware = new ErrorHandlerMw(logger.Object);

        //
        await middleware.InvokeAsync(context, next.Object);

        //
        context.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task InvokeAsync_StatusCode403_ForbidExceptionCaught(){
        //
        var context = new DefaultHttpContext();
        context.Response.StatusCode = 200;

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(It.IsAny<HttpContext>())).ThrowsAsync(new ForbidException("simulation-user", "simulation-service", "simulation-entity", "simulation-entity-id"));

        var logger = new Mock<ILogger<ErrorHandlerMw>>();
        var middleware = new ErrorHandlerMw(logger.Object);

        //
        await middleware.InvokeAsync(context, next.Object);

        //
        context.Response.StatusCode.Should().Be(403);
    }
}
