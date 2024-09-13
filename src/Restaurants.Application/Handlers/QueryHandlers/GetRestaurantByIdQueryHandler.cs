using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers.QueryHandlers;

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
        // Map Restaurant type into RestaurantDTO type
        var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);
        return  restaurantDTO;
    }
}
