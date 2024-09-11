using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
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

        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute]int id){
            var restaurant = await restaurantsService.GetRestaurantAsync(id);
            if(restaurant == null){
                return NotFound($"Restaurant with id {id} not found");
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurantAsync([FromBody] CreateRestaurantDTO createRestaurantDTO){
            var newItem = await restaurantsService.CreateRestaurantAsync(createRestaurantDTO);
            if(newItem == null){
                return Conflict("Server couldn't create requested resource");
            }
            return CreatedAtRoute("GetRestaurant", new{id = newItem.Id}, newItem);
        }
    }
}
