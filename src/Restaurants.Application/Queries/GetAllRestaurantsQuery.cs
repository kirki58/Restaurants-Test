using MediatR;
using Restaurants.API.DTOs.Common;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries;

public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDTO>>
{
    public int PageSize { get; set; }
    public int PageNo { get; set; }
    public string? SearchPhrase { get; set; }
    public int? Category { get; set; } 
}
