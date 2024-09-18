using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class UnassignRoleCommandHandler(
    ILogger<UnassignRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<UnassignRoleCommand>
{
    public async Task Handle(UnassignRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassigning role: {RoleName} from User with Email: {UserEmail}", request.roleName, request.userEmail);

        var user = await userManager.FindByEmailAsync(request.userEmail) ?? throw new NotFoundException(nameof(User), request.userEmail);

        if(!await roleManager.RoleExistsAsync(request.roleName)){
            throw new NotFoundException(nameof(IdentityRole), request.roleName);
        }

        await userManager.RemoveFromRoleAsync(user, request.roleName);
    }
}
