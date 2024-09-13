using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllAsync(){
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute]int id){
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            if(restaurant == null){
                return NotFound($"Restaurant with id {id} not found");
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurantAsync([FromBody] CreateRestaurantCommand command){
            var newItem = await mediator.Send(command);
            if(newItem == null){
                return Conflict("Server couldn't create requested resource");
            }
            return CreatedAtRoute("GetRestaurant", new{id = newItem.Id}, newItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantAsync([FromRoute] int id){
            bool isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

            return isDeleted ? NoContent() : NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id, [FromBody] UpdateRestaurantDTO dto){
            UpdateRestaurantCommand command = new UpdateRestaurantCommand(id, dto);
            bool isUpdated = await mediator.Send(command);

            return isUpdated ? NoContent() : NotFound();
        }
    }
}
