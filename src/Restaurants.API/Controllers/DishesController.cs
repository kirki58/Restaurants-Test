using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.Commands;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateDishAsync([FromRoute] int restaurantId ,CreateDishDTO dto){
            var command = new CreateDishCommand(restaurantId, dto);
            await mediator.Send(command);

            return Created();
        }
    }
}
