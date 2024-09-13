using System;
using AutoMapper;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        // UpdateRestaurantCommand to Restaurant
        CreateMap<UpdateRestaurantCommand, Restaurant>()
            .ForMember(dest => dest.Name, opt => opt.Condition((src, dest) => src.Name != null))
            .ForMember(dest => dest.Description, opt => opt.Condition((src, dest) => src.Description != null))
            .ForMember(dest => dest.HasDelivery, opt => opt.Condition((src, dest) => src.HasDelivery != null));
        // CreateRestaurantDTO to Restaurant
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(
                src => new Address{
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode
                }))
            // Note that CreateDTO.Category string is already validated by FluentValidation before parsing
            .ForMember(dest => dest.Category, opt => opt.MapFrom(
                src => (RestaurantCategory) Enum.Parse(typeof(RestaurantCategory), src.Category)
            ));

        // Restaurant to RestaurantDTO
        CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(dto => dto.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
}
