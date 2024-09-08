using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Restaurants.Infrastructure.Persistence;


// This class generates appropiate DbContext in design time to be used in EFCore migrations.
internal class RestaurantsDbContextFactory : IDesignTimeDbContextFactory<RestaurantsDbContext>
{
    RestaurantsDbContext IDesignTimeDbContextFactory<RestaurantsDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RestaurantsDbContext>();
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("RestaurantsDb"));

        return new RestaurantsDbContext(optionsBuilder.Options);
    }
}
