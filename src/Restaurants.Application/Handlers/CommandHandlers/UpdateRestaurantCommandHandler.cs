using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.Services;
using Restaurants.Application.Users;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
 IRestaurantsRepository repository,
 IMapper mapper,
 IUserContext userContext) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Restaurant entity with ID: {RestaurantID} with values {@NewValues}", request.Id, request);
        var restaurant = await repository.GetRestaurantByIdAsync(request.Id);

        if(restaurant == null){
            logger.LogWarning($"Restaurant with ID {request.Id} not found");
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        var user = userContext.GetCurrentUser();
        if(!user.IsAuthorized(restaurant)){
            throw new ForbidException(user.Id, this.GetType().Name, nameof(Restaurant), request.Id.ToString());
        }
        

        mapper.Map(request, restaurant);

        await repository.UpdateRestaurantAsync(restaurant);
    }
}
