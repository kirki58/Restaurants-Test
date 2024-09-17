using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Handlers.CommandHandlers;

public class UpdateUserDetailsCommandHandler(
    ILogger<UpdateUserDetailsCommand> logger,
    IUserStore<User> userStore,
    IUserContext userContext
) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating BirthDate and Nationality values for user with Id : {UserId}, Serialized as: {User}", user.Id, user);

        var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken);
        if(dbUser == null){
            throw new NotFoundException(nameof(User), user.Id);
        }

        dbUser.BirthDate = request.BirthDate;
        dbUser.Nationality = request.Nationality;
        
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
