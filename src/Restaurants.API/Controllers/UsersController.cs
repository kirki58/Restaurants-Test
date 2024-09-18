using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands;
using Restaurants.Application.Validators;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command){
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("role")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> AssignRole(AssignRoleCommand command){
            await mediator.Send(command);
            return Created();
        }

        [HttpDelete("role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> UnassignRole(UnassignRoleCommand command){
            await mediator.Send(command);
            return NoContent();
        }
    }
}
