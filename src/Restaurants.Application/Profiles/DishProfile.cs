using System;
using AutoMapper;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Profiles;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDTO>();
    }
}
