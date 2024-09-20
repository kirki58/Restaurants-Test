using System;
using FluentValidation;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;

namespace Restaurants.Application.Validators;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(q => q.PageSize)
        .NotEmpty()
        .WithMessage("Specifying a page size is mandatory")
        .LessThanOrEqualTo(50)
        .WithMessage("Page size should at most be 50")
        .GreaterThanOrEqualTo(1)
        .WithMessage("Page size should at least be 1");

        RuleFor(q => q.PageNo)
        .NotEmpty()
        .WithMessage("Specifying a page number is mandatory")
        .GreaterThanOrEqualTo(1)
        .WithMessage("Page number should at least be 1");


        List<string> acceptedSortColumns = [nameof(RestaurantDTO.Name), nameof(RestaurantDTO.Tables), nameof(RestaurantDTO.Dishes)];

        RuleFor(q => q.sortBy!)
        .Must(value => acceptedSortColumns.Contains(value))
        .When(q => q.sortBy != null)
        .WithMessage($"Sort by should be in {string.Join(", ", acceptedSortColumns)} when specified.");

        RuleFor(q => q.sortDesc)
        .Must(value => value == null)
        .When(q => q.sortBy == null)
        .WithMessage("Can sort descending when only sortby parameter is specified.");
    }
}
