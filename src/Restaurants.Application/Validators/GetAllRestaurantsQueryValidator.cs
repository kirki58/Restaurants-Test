using System;
using FluentValidation;
using Restaurants.Application.Queries;

namespace Restaurants.Application.Validators;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(q => q.PageSize)
        .NotEmpty()
        .WithMessage("Specifying a page size is mandatory")
        .GreaterThanOrEqualTo(1)
        .WithMessage("Page size should at least be 1");

        RuleFor(q => q.PageNo)
        .NotEmpty()
        .WithMessage("Specifying a page number is mandatory")
        .GreaterThanOrEqualTo(1)
        .WithMessage("Page number should at least be 1");
    }
}
