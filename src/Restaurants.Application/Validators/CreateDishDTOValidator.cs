using FluentValidation;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Validators;

public class CreateDishDTOValidator : AbstractValidator<CreateDishDTO>
{
    public CreateDishDTOValidator()
    {
        RuleFor(dto => dto.Name)
        .NotEmpty().WithMessage("Name field should be provided")
        .Length(3,50).WithMessage("Name of the dish must be between 3-50 characters");

        RuleFor(dto => dto.Description)
        .NotEmpty().WithMessage("Decription field should be provided")
        .Length(3,500).WithMessage("Description of the dish must be between 3-500");

        RuleFor(dto => dto.Price)
        .GreaterThanOrEqualTo(0).WithMessage("Price of the dish must be a positive number");

        RuleFor(dto => dto.Kcal)
        .GreaterThanOrEqualTo(0).WithMessage("Kilocalories of a dish must be a positive number (if provided)");
    }
}
