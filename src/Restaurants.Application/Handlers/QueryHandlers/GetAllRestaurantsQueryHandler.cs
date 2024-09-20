using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class GetAllRestaurantsQueryHandler(
    ILogger<GetAllRestaurantsQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository repository
) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
{
    public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching All Restaurants From Database");
        var restaurants = await repository.GetAllAsync(request.SearchParams, request.Category);
        // Map Restaurant type into RestaurantDTO
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        return restaurantDtos;
    }
}
