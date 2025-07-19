using FixLife.Admin.Users.Abstraction;
using FixLife.Admin.Users.Exceptions;
using FluentAssertions;
using Moq;

namespace FixLife.Admin.UnitTests.Users;

public class UsersTests
{
    private readonly Mock<IClientUserService> _mockClientUserService;

    public UsersTests()
    {
        _mockClientUserService = new Mock<IClientUserService>();
    }

    [Fact]
    public async Task ResetUserPassword_WithValidUserId_ShouldReturnSuccessStatus()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockClientUserService
            .Setup(x => x.ResetUserPassword(userId))
            .ReturnsAsync(((short)200, "Redirect to page!"));

        // Act
        var result = await _mockClientUserService.Object.ResetUserPassword(userId);

        // Assert
        result.Item1.Should().Be(200);
        result.Item2.Should().Be("Redirect to page!");
        _mockClientUserService.Verify(x => x.ResetUserPassword(userId), Times.Once);
    }

    [Fact]
    public async Task ResetUserPassword_WithInvalidUserId_ShouldThrowClientNotFoundException()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();
        _mockClientUserService
            .Setup(x => x.ResetUserPassword(nonExistentUserId))
            .ThrowsAsync(new ClientNotFoundException());

        // Act & Assert
        await FluentActions.Invoking(() => _mockClientUserService.Object.ResetUserPassword(nonExistentUserId))
            .Should().ThrowAsync<ClientNotFoundException>();
        
        _mockClientUserService.Verify(x => x.ResetUserPassword(nonExistentUserId), Times.Once);
    }

    [Fact]
    public async Task ConfirmUserPassword_WithValidUserIdAndPassword_ShouldReturnSuccessStatus()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var testPassword = "TestPassword123!";
        _mockClientUserService
            .Setup(x => x.ConfirmUserPassword(userId, testPassword))
            .ReturnsAsync(((short)200, "Password confirmed"));

        // Act
        var result = await _mockClientUserService.Object.ConfirmUserPassword(userId, testPassword);

        // Assert
        result.Item1.Should().Be(200);
        result.Item2.Should().Be("Password confirmed");
        _mockClientUserService.Verify(x => x.ConfirmUserPassword(userId, testPassword), Times.Once);
    }

    [Fact]
    public async Task ConfirmUserPassword_WithInvalidUserId_ShouldThrowClientNotFoundException()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();
        var testPassword = "TestPassword123!";
        _mockClientUserService
            .Setup(x => x.ConfirmUserPassword(nonExistentUserId, testPassword))
            .ThrowsAsync(new ClientNotFoundException());

        // Act & Assert
        await FluentActions.Invoking(() => _mockClientUserService.Object.ConfirmUserPassword(nonExistentUserId, testPassword))
            .Should().ThrowAsync<ClientNotFoundException>();
        
        _mockClientUserService.Verify(x => x.ConfirmUserPassword(nonExistentUserId, testPassword), Times.Once);
    }

    [Fact]
    public async Task LogoutForce_WithValidUserId_ShouldCallHttpServiceAndReturnResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedResult = ((short)200, "User logged out successfully");
        _mockClientUserService
            .Setup(x => x.LogoutForce(userId))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _mockClientUserService.Object.LogoutForce(userId);

        // Assert
        result.Should().Be(expectedResult);
        _mockClientUserService.Verify(x => x.LogoutForce(userId), Times.Once);
    }

    [Fact]
    public async Task LogoutForce_WithInvalidUserId_ShouldThrowClientNotFoundException()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();
        _mockClientUserService
            .Setup(x => x.LogoutForce(nonExistentUserId))
            .ThrowsAsync(new ClientNotFoundException());

        // Act & Assert
        await FluentActions.Invoking(() => _mockClientUserService.Object.LogoutForce(nonExistentUserId))
            .Should().ThrowAsync<ClientNotFoundException>();
        
        _mockClientUserService.Verify(x => x.LogoutForce(nonExistentUserId), Times.Once);
    }

    [Fact]
    public async Task ModifyUser_WithValidUserIdAndUserData_ShouldUpdateUserAndReturnSuccess()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updatedUserData = new FixLife.Admin.Users.Models.ClientUser
        {
            Id = userId,
            Name = "UpdatedName",
            Surname = "UpdatedSurname",
            Email = "updated.email@example.com",
            PhoneNumber = "999888777"
        };
        _mockClientUserService
            .Setup(x => x.ModifyUser(userId, updatedUserData))
            .ReturnsAsync(((short)0, "User updated"));

        // Act
        var result = await _mockClientUserService.Object.ModifyUser(userId, updatedUserData);

        // Assert
        result.Item1.Should().Be(0);
        result.Item2.Should().Be("User updated");
        _mockClientUserService.Verify(x => x.ModifyUser(userId, updatedUserData), Times.Once);
    }

    [Fact]
    public async Task ModifyUser_WithInvalidUserId_ShouldThrowClientNotFoundException()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();
        var userData = new FixLife.Admin.Users.Models.ClientUser
        {
            Id = nonExistentUserId,
            Name = "TestName",
            Surname = "TestSurname",
            Email = "test@example.com",
            PhoneNumber = "123456789"
        };
        _mockClientUserService
            .Setup(x => x.ModifyUser(nonExistentUserId, userData))
            .ThrowsAsync(new ClientNotFoundException());

        // Act & Assert
        await FluentActions.Invoking(() => _mockClientUserService.Object.ModifyUser(nonExistentUserId, userData))
            .Should().ThrowAsync<ClientNotFoundException>();
        
        _mockClientUserService.Verify(x => x.ModifyUser(nonExistentUserId, userData), Times.Once);
    }

    [Theory]
    [InlineData("", "ValidSurname", "valid@email.com", "123456789")]
    [InlineData("ValidName", "", "valid@email.com", "123456789")]
    [InlineData("ValidName", "ValidSurname", "", "123456789")]
    [InlineData("ValidName", "ValidSurname", "valid@email.com", "")]
    public async Task ModifyUser_WithInvalidUserData_ShouldStillProcessUpdate(
        string name, string surname, string email, string phoneNumber)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updatedUserData = new FixLife.Admin.Users.Models.ClientUser
        {
            Id = userId,
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phoneNumber
        };
        _mockClientUserService
            .Setup(x => x.ModifyUser(userId, updatedUserData))
            .ReturnsAsync(((short)0, "User updated"));

        // Act
        var result = await _mockClientUserService.Object.ModifyUser(userId, updatedUserData);

        // Assert
        result.Item1.Should().Be(0);
        result.Item2.Should().Be("User updated");
        _mockClientUserService.Verify(x => x.ModifyUser(userId, updatedUserData), Times.Once);
    }

    [Fact]
    public async Task ResetUserPassword_MultipleCallsWithSameUserId_ShouldReturnConsistentResults()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockClientUserService
            .Setup(x => x.ResetUserPassword(userId))
            .ReturnsAsync(((short)200, "Redirect to page!"));

        // Act
        var result1 = await _mockClientUserService.Object.ResetUserPassword(userId);
        var result2 = await _mockClientUserService.Object.ResetUserPassword(userId);

        // Assert
        result1.Should().BeEquivalentTo(result2);
        result1.Item1.Should().Be(200);
        result1.Item2.Should().Be("Redirect to page!");
        _mockClientUserService.Verify(x => x.ResetUserPassword(userId), Times.Exactly(2));
    }

    [Fact]
    public async Task ConfirmUserPassword_WithNullOrEmptyPassword_ShouldHandleGracefully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockClientUserService
            .Setup(x => x.ConfirmUserPassword(userId, null!))
            .ReturnsAsync(((short)400, "Invalid password"));
        _mockClientUserService
            .Setup(x => x.ConfirmUserPassword(userId, ""))
            .ReturnsAsync(((short)400, "Invalid password"));

        // Act & Assert for null password
        var nullResult = await _mockClientUserService.Object.ConfirmUserPassword(userId, null!);
        nullResult.Item1.Should().Be(400);
        nullResult.Item2.Should().Be("Invalid password");

        // Act & Assert for empty password
        var emptyResult = await _mockClientUserService.Object.ConfirmUserPassword(userId, "");
        emptyResult.Item1.Should().Be(400);
        emptyResult.Item2.Should().Be("Invalid password");

        _mockClientUserService.Verify(x => x.ConfirmUserPassword(userId, null!), Times.Once);
        _mockClientUserService.Verify(x => x.ConfirmUserPassword(userId, ""), Times.Once);
    }

    [Fact]
    public async Task ResetUserPassword_ShouldVerifyCorrectMethodCall()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockClientUserService
            .Setup(x => x.ResetUserPassword(It.IsAny<Guid>()))
            .ReturnsAsync(((short)200, "Redirect to page!"));

        // Act
        await _mockClientUserService.Object.ResetUserPassword(userId);

        // Assert
        _mockClientUserService.Verify(x => x.ResetUserPassword(userId), Times.Once);
        _mockClientUserService.Verify(x => x.ResetUserPassword(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task AllServiceMethods_ShouldBeTestable()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userData = new FixLife.Admin.Users.Models.ClientUser
        {
            Id = userId,
            Name = "Test",
            Surname = "User",
            Email = "test@example.com",
            PhoneNumber = "123456789"
        };
        var password = "TestPassword123!";

        // Setup all methods
        _mockClientUserService.Setup(x => x.ResetUserPassword(userId)).ReturnsAsync(((short)200, "Reset successful"));
        _mockClientUserService.Setup(x => x.ConfirmUserPassword(userId, password)).ReturnsAsync(((short)200, "Password confirmed"));
        _mockClientUserService.Setup(x => x.LogoutForce(userId)).ReturnsAsync(((short)200, "Logout successful"));
        _mockClientUserService.Setup(x => x.ModifyUser(userId, userData)).ReturnsAsync(((short)0, "User modified"));

        // Act & Assert
        var resetResult = await _mockClientUserService.Object.ResetUserPassword(userId);
        var confirmResult = await _mockClientUserService.Object.ConfirmUserPassword(userId, password);
        var logoutResult = await _mockClientUserService.Object.LogoutForce(userId);
        var modifyResult = await _mockClientUserService.Object.ModifyUser(userId, userData);

        // Assert all methods were called and returned expected results
        resetResult.Item1.Should().Be(200);
        confirmResult.Item1.Should().Be(200);
        logoutResult.Item1.Should().Be(200);
        modifyResult.Item1.Should().Be(0);

        _mockClientUserService.Verify(x => x.ResetUserPassword(userId), Times.Once);
        _mockClientUserService.Verify(x => x.ConfirmUserPassword(userId, password), Times.Once);
        _mockClientUserService.Verify(x => x.LogoutForce(userId), Times.Once);
        _mockClientUserService.Verify(x => x.ModifyUser(userId, userData), Times.Once);
    }
}