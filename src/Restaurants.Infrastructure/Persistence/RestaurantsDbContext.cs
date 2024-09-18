using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Entitites;

namespace Restaurants.Infrastructure.Persistence;


// IdentityDbContext inherits DbContext, it has built-in auth logic
internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);
        
        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.OwnedRestaurants)
            .WithOne(r => r.RestaurantAdmin)
            .HasForeignKey(r => r.AdminId);
    }

    internal async static Task SeedData(RestaurantsDbContext context){
        // Ensure that the DB is up to date before seeding data
        // Pending migrations are directly applied (if any)
        await context.Database.MigrateAsync();

        if(!context.Restaurants.Any()){
            await context.Restaurants.AddRangeAsync(context.GetSeedData());
            await context.SaveChangesAsync();
        }

        if(!context.Roles.Any()){
            List<IdentityRole> roles = [
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = AppRoles.User,
                    NormalizedName = AppRoles.User.ToUpper(),
                },
                
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = AppRoles.Admin,
                    NormalizedName = AppRoles.Admin.ToUpper(),
                }
            ];
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }
    }

    private IEnumerable<Restaurant> GetSeedData(){
        List<Restaurant> seedData = [
            new Restaurant {
                Name = "McDonald's",
                Description = "Amazing burger fast-food chain",
                Category = RestaurantCategory.FastFood,
                HasDelivery = true,
                ContactNumber = "111",
                ContactEmail = "sarigazi@mcdonals.com",
                Address = new Address{City = "Istanbul", Street = "Demokrasi St.", PostalCode="3434"},
                Dishes = [
                    new Dish{Name = "BigMac", Description = "Amazing burger", Price=230m},
                    new Dish{Name= "BBQ Burger", Description="Burger with BBQ sauce", Price=200m},
                    new Dish{Name= "Ice Cream", Description="MC creamy ice cream", Price=50m}
                ],
                Tables = 50
            },
            new Restaurant {
                Name = "Murat Chef",
                Description = "Perfect Turkish steaks",
                Category = RestaurantCategory.Turkish,
                HasDelivery = true,
                ContactNumber = "222",
                ContactEmail = "muratchef@business.com",
                Address = new Address{City = "Istanbul", Street = "Barajyolu Blv.", PostalCode="3434"},
                Dishes = [
                    new Dish{Name = "Medium Steak", Description = "Medium-cooked steak", Price=500m},
                    new Dish{Name= "Mayo Steak", Description="Steak with awesome mayo sauce", Price=600m},
                    new Dish{Name= "Şiş Kebap", Description="Turkish traditional beef shish", Price=400m}
                ]
            }
            
        ];
        return seedData;
    }
}
