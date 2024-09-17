using MediatR;

namespace Restaurants.Application.Commands;

public class UpdateUserDetailsCommand: IRequest
{
    public DateOnly? BirthDate { get; set; }
    public string? Nationality { get; set; }
}
