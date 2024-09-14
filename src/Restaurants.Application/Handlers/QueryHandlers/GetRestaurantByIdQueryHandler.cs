using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class GetRestaurantByIdQueryHandler(
    ILogger<GetRestaurantByIdQueryHandler> logger,
    IRestaurantsRepository repository,
    IMapper mapper
) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO>
{
    public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching Restaurant with ID: {RestaurantId}", request.Id);
        var restaurant = await repository.GetRestaurantByIdAsync(request.Id);

        if(restaurant == null){
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }
        // Map Restaurant type into RestaurantDTO type
        var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);
        return  restaurantDTO;
    }
}
