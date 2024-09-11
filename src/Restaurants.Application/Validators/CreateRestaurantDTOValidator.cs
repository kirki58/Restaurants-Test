using FluentValidation;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Validators;

public class CreateRestaurantDTOValidator : AbstractValidator<CreateRestaurantDTO>
{
    public CreateRestaurantDTOValidator()
    {
        RuleFor(dto => dto.Name)
        .NotEmpty()
        .Length(3,100).WithMessage("Length of the Name property must be between 3-100");

        RuleFor(dto => dto.Description)
        .NotEmpty()
        .Length(0,5000).WithMessage("Description length can have max of 5000 characters.");

        RuleFor(dto => dto.Category)
        .NotEmpty()
        .Must(category => Enum.TryParse<RestaurantCategory>(category, true, out _)).WithMessage("Provided category is not a valid restaurant category!");

        RuleFor(dto => dto.ContactNumber)
        .Matches("^5\\d{9}$").WithMessage("Phone number is not valid!");

        RuleFor(dto => dto.ContactEmail)
        .EmailAddress().WithMessage("Email address must be valid");

        RuleFor(dto => dto.PostalCode)
        .Matches("^\\d{5}$").WithMessage("Postal Code must be a 5 digit number.");

        RuleFor(dto => dto.Tables)
        .GreaterThan(-1).WithMessage("Tables cant be negative.");
    }
}
