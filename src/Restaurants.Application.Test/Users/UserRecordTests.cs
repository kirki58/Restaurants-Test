using Restaurants.Application.Users;
using FluentAssertions;
using Restaurants.Domain.Entities;


namespace Restaurants.Application.Test.Users;

public class UserRecordTests
{
    // Naming Convention: MethodName_StateUnderTest_ExpectedBehavior

    [Theory] // Theory is used when you want to test the same method with different inputs. Theory is for tests that are driven by data.
    [InlineData(AppRoles.Admin)]
    [InlineData(AppRoles.User)]
    public void IsInRole_ValidRole_ReturnsTrue(string role)
    {
        // Arrange: This section is where we set up, variables, objects, and other resources needed to execute the test.
        var userRecord = new UserRecord("testId", "test@testuser.com", [AppRoles.Admin, AppRoles.User]);

        // Act: This section is where we execute the code that we are testing.
        var result = userRecord.IsInRole(role);

        // Assert: This section is where we verify that the code executed in the Act section behaved as expected. (With FluentAssertions package in this case.)
        result.Should().BeTrue();
    }

    [Fact] // Fact is used when you want to test a single method with a single input. Fact is for tests that are not driven by data.
    public void IsInRole_InvalidRole_ReturnsFalse()
    {
        // Arrange
        var userRecord = new UserRecord("testId", "test@testuser.com", [AppRoles.User]);

        // Act
        var result = userRecord.IsInRole(AppRoles.Admin); // User doesn't have the AppRoles.Admin role, the expected result is false.

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_ValidRoleWithLowerCase_ReturnsFalse(){
        // Arrange
        var userRecord = new UserRecord("testId", "test@testuser.com", [AppRoles.Admin, AppRoles.User]);

        // Act
        var result = userRecord.IsInRole(AppRoles.Admin.ToLower()); // User is actually in role "Admin", but the input parameter is given with an invalid string (upper-lower case differences)

        // Assert
        result.Should().BeFalse();
    }
}
