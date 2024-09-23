using FluentAssertions;
using FluentValidation.TestHelper;
using Restaurants.Application.Commands;
using Restaurants.Application.Validators;
using Restaurants.Domain.Entitites;

namespace Restaurants.Application.Test.Validators;

public class CreateRestaurantDTOValidatorTests
{
    [Fact]
    public void Validator_ValidCommand_ShouldSuceed(){
        //
        var dto = new CreateRestaurantCommand{
            Category = "Turkish",                   // Valid
            City = "Istanbul",                      // Valid
            ContactEmail = "test@testemail.com",    // Valid
            ContactNumber = "5555555555",           // Valid
            Description = "Valid desc",             // Valid
            HasDelivery = true,                     // Valid
            PostalCode = "12345",                   // Valid
            Street = "Test street",                 // Valid
            Tables = 50,                            // Valid
            Name = "Valid Name"                     // Valid
        };

        var validator = new CreateRestaurantDTOValidator();

        //
        var result = validator.TestValidate(dto);

        //
        result.ShouldNotHaveAnyValidationErrors();
    }


    [Theory]
    [InlineData(-1)] // represents null string
    [InlineData(1)]
    [InlineData(101)]
    // Rule For Name prop: Name should not be null, it should be at least 3 characters and at most 100 characters long.  
    public void Validator_InvalidName_ShouldFail(int length){
        // Arrange
        string? testName;
        if(length == -1){
            testName = null;
        }
        else{
            testName = new string('a', length);
        }
        var validator = new CreateRestaurantDTOValidator();

        var dto = new CreateRestaurantCommand{
            Category = "category",
            Description = "Valid desc",

            Name = testName                         // Invalid
        };
        // Act
        var result = validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor("Name");
    }

    [Fact]
    public void Validator_InvalidCommand_ShouldFail(){
        //
        var dto = new CreateRestaurantCommand{
            Category = "invalid category",
            ContactEmail = "test",
            ContactNumber = "123",
            Description = new string('a', 5001),
            PostalCode = "1",
            Tables = -1,
            Name = "a"
        };

        var validator = new CreateRestaurantDTOValidator();

        //
        var result = validator.TestValidate(dto);

        //
        result.ShouldHaveValidationErrorFor(r => r.Category);
        result.ShouldHaveValidationErrorFor(r => r.ContactEmail);
        result.ShouldHaveValidationErrorFor(r => r.ContactNumber);
        result.ShouldHaveValidationErrorFor(r => r.Description);
        result.ShouldHaveValidationErrorFor(r => r.PostalCode);
        result.ShouldHaveValidationErrorFor(r => r.Tables);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Theory]
    [InlineData("Turkish")]
    [InlineData("Italian")]
    [InlineData("FastFood")]
    [InlineData("Chinese")]
    public void Validator_ValidCategory_ShouldSuceed(string testCategory){
        // Arrange
        var dto = new CreateRestaurantCommand{
            Category = testCategory,
            Description = new string('a', 5001),
            Name = "name"
        };
        var validator = new CreateRestaurantDTOValidator();
        // Act
        var result = validator.TestValidate(dto);
        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.Category);
    }
}
