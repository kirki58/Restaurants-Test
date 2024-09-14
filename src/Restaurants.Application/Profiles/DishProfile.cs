using System;
using AutoMapper;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Profiles;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<CreateDishCommand, Dish>();
        CreateMap<Dish, DishDTO>();
    }
}
