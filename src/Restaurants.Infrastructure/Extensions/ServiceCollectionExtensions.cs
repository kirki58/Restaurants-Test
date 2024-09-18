using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Autharization;
using Restaurants.Infrastructure.Autharization.Constants;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfraStructure(this IServiceCollection services){
        services.AddDbContext<RestaurantsDbContext>( options => 
            options
                .UseSqlServer(Environment.GetEnvironmentVariable("RestaurantsDb"))
                .EnableSensitiveDataLogging()
        );

        // Add AspNetCore.Identity.EntityFrameworkCore API User Endpoints bound to repositories of RestaurantsDbContext
        services.AddIdentityApiEndpoints<User>()
        .AddRoles<IdentityRole>()
        .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>()
        .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddAuthorizationBuilder()
        .AddPolicy(AppPolicies.HasNationality, policy => policy.RequireClaim(AppClaimTypes.Nationality))
        .AddPolicy(AppPolicies.IsTurkish, policy => policy.RequireClaim(AppClaimTypes.Nationality, "Turkish"))
        .AddPolicy(AppPolicies.OlderThanEighteen, policy => policy.RequireAssertion(context => { 
            var birthDate = context.User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.BirthDate);

            if(birthDate == null){
                return false;
            }
            
            var parsedDate = DateOnly.Parse(birthDate.Value);
            var now = DateOnly.FromDateTime(DateTime.Now);

            var passedYears = now.Year - parsedDate.Year;
             
            if(now < parsedDate.AddYears(passedYears)){
                passedYears--;
            }

            return passedYears >= 18;
        }));
        

        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
    }
}
