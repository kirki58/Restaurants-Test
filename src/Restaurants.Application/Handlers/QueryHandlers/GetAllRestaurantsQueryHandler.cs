using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.API.DTOs.Common;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers;

public class GetAllRestaurantsQueryHandler(
    ILogger<GetAllRestaurantsQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository repository
) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDTO>>
{
    public async Task<PagedResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching All Restaurants From Database");
        var (restaurants, count) = await repository.GetAllAsync(request.PageSize,request.PageNo,request.SearchPhrase, request.Category, request.sortBy, request.sortDesc);
        // Map Restaurant type into RestaurantDTO
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

        var pagedResult = new PagedResult<RestaurantDTO>(restaurantDtos, count, request.PageSize, request.PageNo);
        return pagedResult;
    }
}
