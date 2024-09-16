using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;
using Restaurants.Domain.Entitites;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Handlers.QueryHandlers;

public class GetDishByIdQueryHandler(
    ILogger<GetDishByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository
    ) : IRequestHandler<GetDishByIdQuery, DishDTO>
{
    public async Task<DishDTO> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching specific dish with id: {DishId} belonging to Restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant == null){
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        
        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if(dish == null){
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        return mapper.Map<DishDTO>(dish);
    }
}
