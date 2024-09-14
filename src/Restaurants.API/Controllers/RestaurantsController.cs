using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDTO>))]
        public async Task<IActionResult> GetAllAsync(){
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}", Name = "GetRestaurant")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute]int id){
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(restaurant);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateRestaurantAsync([FromBody] CreateRestaurantCommand command){
            var newItem = await mediator.Send(command);
            return CreatedAtRoute("GetRestaurant", new{id = newItem.Id}, newItem);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurantAsync([FromRoute] int id){
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id, [FromBody] UpdateRestaurantDTO dto){
            UpdateRestaurantCommand command = new UpdateRestaurantCommand(id, dto);
            await mediator.Send(command);

            return NoContent();
        }
    }
}
