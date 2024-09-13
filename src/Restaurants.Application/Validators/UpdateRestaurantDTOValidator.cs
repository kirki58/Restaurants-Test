using System;
using FluentValidation;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Validators;

public class UpdateRestaurantDTOValidator : AbstractValidator<UpdateRestaurantDTO>
{
    public UpdateRestaurantDTOValidator()
    {
        RuleFor(dto => dto.Name)
        .Length(3,100).WithMessage("Length of the Name property must be between 3-100");

        RuleFor(dto => dto.Description)
        .Length(0,5000).WithMessage("Description length can have max of 5000 characters.");
    }
}
