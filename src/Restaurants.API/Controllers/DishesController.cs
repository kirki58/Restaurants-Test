using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.Commands;
using Restaurants.Application.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize] // Authorizes Admin or restaurant owner internally 
        public async Task<IActionResult> CreateDishAsync([FromRoute] int restaurantId ,CreateDishDTO dto){
            var command = new CreateDishCommand(restaurantId, dto);
            var newItem = await mediator.Send(command);

            return CreatedAtRoute("GetDishSpec", new { restaurantId, dishId = newItem.Id }, newItem);
        }

        [HttpGet("{dishId}", Name = "GetDishSpec")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDishByIdAsync([FromRoute] int restaurantId, [FromRoute] int dishId){
            var query = new GetDishByIdQuery(restaurantId, dishId);
            var dish = await mediator.Send(query);

            return Ok(dish);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DishDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllDishesAsync([FromRoute] int restaurantId){
            var query = new GetAllDishesQuery(restaurantId);
            var dishes = await mediator.Send(query);

            return Ok(dishes);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize] // Authorizes Admin or restaurant owner internally 
        public async Task<IActionResult> DeleteAllDishesAsync([FromRoute] int restaurantId){
            var command = new DeleteDishesCommand(restaurantId);
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{dishId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize] // Authorizes Admin or restaurant owner internally 
        public async Task<IActionResult> DeleteDishByIdAsync([FromRoute] int restaurantId, [FromRoute] int dishId){
            var command = new DeleteDishByIdCommand(restaurantId, dishId);
            await mediator.Send(command);

            return NoContent();
        }
    }
}
