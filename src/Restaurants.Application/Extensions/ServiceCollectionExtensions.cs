using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Profiles;
using Restaurants.Application.Services;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services){
        services.AddScoped<IRestaurantsService, RestaurantsService>();

        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly)
        .AddFluentValidationAutoValidation();
    }
}
