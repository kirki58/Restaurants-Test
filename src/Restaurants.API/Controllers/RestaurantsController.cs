using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Services;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(){
            var restaurants = await restaurantsService.GetRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute] int id){
            var restaurant = await restaurantsService.GetRestaurantAsync(id);
            if(restaurant == null){
                return NotFound($"Restaurant with id {id} not found");
            }
            return Ok(restaurant);
        }
    }
}
