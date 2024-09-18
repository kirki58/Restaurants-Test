using System;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class AssignRoleCommandHandler
(
    ILogger<AssignRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
) : IRequestHandler<AssignRoleCommand>
{
    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assignig role with Name: {RoleName} for user with Email: {UserName}", request.roleName, request.userEmail);

        var user = await userManager.FindByEmailAsync(request.userEmail) ?? throw new NotFoundException(nameof(User), request.userEmail);

        if(!await roleManager.RoleExistsAsync(request.roleName)){
            throw new NotFoundException(nameof(IdentityRole), request.roleName);
        }

        await userManager.AddToRoleAsync(user, request.roleName);
    }
}
