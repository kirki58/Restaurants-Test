using System;
using MediatR;

namespace Restaurants.Application.Commands;

public class UnassignRoleCommand : IRequest
{
    public required string userEmail { get; set; }
    public required string roleName { get; set; }
}
