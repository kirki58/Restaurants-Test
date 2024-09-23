using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Commands;
using Restaurants.Application.DTOs;
using Restaurants.Application.Profiles;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Test.Mapping;

public class RestaurantMappingTests
{
    private readonly IMapper _mapper;
    public RestaurantMappingTests()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<RestaurantProfile>();
            cfg.AddProfile<DishProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CreateMap_FromCreateRestaurantCommandToRestaurant_MapsCorrectly(){
        //
        var testCommand = new CreateRestaurantCommand{
            City = "city",
            PostalCode = "12345",
            Street = "street",
            Category = "Italian",
            ContactEmail = "test@testrestaurant.com",
            ContactNumber = "5554443322",
            Description = "test description",
            HasDelivery = true,
            Name = "restaurant",
            Tables = 12
        };

        //
        var restaurant = _mapper.Map<Restaurant>(testCommand);

        //
        restaurant.Should().NotBeNull();
        restaurant.Address?.City.Should().Be(testCommand.City);
        restaurant.Address?.Street.Should().Be(testCommand.Street);
        restaurant.Address?.PostalCode.Should().Be(testCommand.PostalCode);
        restaurant.Category.Should().Be(Enum.Parse<RestaurantCategory>(testCommand.Category));
        restaurant.ContactNumber.Should().Be(testCommand.ContactNumber);
        restaurant.ContactNumber.Should().Be(testCommand.ContactNumber);
        restaurant.Description.Should().Be(testCommand.Description);
        restaurant.HasDelivery.Should().Be(testCommand.HasDelivery);
        restaurant.Name.Should().Be(testCommand.Name);
        restaurant.Tables.Should().Be(testCommand.Tables);
    }

    [Fact]
    public void CreateMap_FromUpdateRestaurantCommandToRestaurantNonNullInput_MapsCorrectly(){
        //
        var testRestaurant = new Restaurant{
            Id = 1,
            Description = "test description",
            HasDelivery = false,
            Name = "restaurant",
        };

        var testDTO = new UpdateRestaurantDTO{
            Name = "name",
            Description = "desc",
            HasDelivery = true
        };
        var testCommand = new UpdateRestaurantCommand(1, testDTO);

        //
        _mapper.Map(testCommand, testRestaurant);

        //
        testRestaurant.Name.Should().Be(testCommand.Name);
        testRestaurant.Description.Should().Be(testCommand.Description);
        testRestaurant.HasDelivery.Should().Be((bool)testCommand.HasDelivery!);
    }

    [Fact]
    public void CreateMap_FromUpdateRestaurantCommandToRestaurantNullInput_MapsCorrectly(){
        //
        var testRestaurant = new Restaurant{
            Id = 1,
            Description = "test description",
            HasDelivery = false,
            Name = "restaurant",
        };

        // Have a sample restaurant to have something to compare the results to later.
        var sampleRestaurant = new Restaurant{
            Id = testRestaurant.Id,
            Name = testRestaurant.Name,
            Description = testRestaurant.Description,
            HasDelivery = testRestaurant.HasDelivery
        };

        var testDTO = new UpdateRestaurantDTO{
            Name = null,
            Description = null,
            HasDelivery = null
        };
        var testCommand = new UpdateRestaurantCommand(1, testDTO);

        //
        _mapper.Map(testCommand, testRestaurant);

        //
        testRestaurant.Name.Should().Be(sampleRestaurant.Name);
        testRestaurant.Description.Should().Be(sampleRestaurant.Description);
        testRestaurant.HasDelivery.Should().Be(sampleRestaurant.HasDelivery);
    }

    [Fact]
    public void CreateMap_FromRestaurantToRestaurantDTO_MapsCorrectly(){
        //
        var testRestaurant = new Restaurant{
            Id = 1,
            Address = new Address{
                City = "city",
                PostalCode = "12345",
                Street = "street"
            },
            AdminId = "adminId",
            Category = 0,
            ContactEmail = "test@testrestaurant.com",
            ContactNumber = "5554443322",
            Description = "test description",
            Dishes = new List<Dish>{new Dish{ Id = 2, Description = "desc", Kcal = 200, Name = "dish", Price = 123.2m, RestaurantId = 1}},
            HasDelivery = true,
            Name = "restaurant",
            Tables = 12
        };

        //
        var resultDto = _mapper.Map<RestaurantDTO>(testRestaurant);

        //
        resultDto.Should().NotBeNull();
        resultDto.Id.Should().Be(testRestaurant.Id);
        resultDto.Dishes.Should().BeEquivalentTo(new List<DishDTO>{
            new DishDTO{ Id = testRestaurant.Dishes[0].Id, Description = testRestaurant.Dishes[0].Description, Kcal = testRestaurant.Dishes[0].Kcal, Name = testRestaurant.Dishes[0].Name, Price = testRestaurant.Dishes[0].Price}
        });
        resultDto.Tables.Should().Be(testRestaurant.Tables);
        resultDto.Address.Should().Be(testRestaurant.Address);
        resultDto.HasDelivery.Should().Be(testRestaurant.HasDelivery);
        resultDto.Name.Should().Be(testRestaurant.Name);
        resultDto.Description.Should().Be(testRestaurant.Description);
        resultDto.Category.Should().Be(testRestaurant.Category);

    }
}
