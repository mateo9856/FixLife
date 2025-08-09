using FixLife.Admin.Db.Entities;
using FixLife.Admin.Users.Abstraction;
using FluentAssertions;
using Moq.EntityFrameworkCore;
using ClientUserDto = FixLife.Admin.Users.Models.ClientUser;

namespace FixLife.Admin.UnitTests.Users;

public class UsersTests : TestBase
{
    private readonly IClientUserService _sut;

    private static readonly Guid ClientId1 = Guid.NewGuid();
    private static readonly Guid ClientId2 = Guid.NewGuid();
    
    public UsersTests(IClientUserService clientUserService)
    {
        _sut = clientUserService;
    }

    protected override void SetupEntity()
    {
        var users = new List<ClientUser>
        {
            new ClientUser
            {
                Id = ClientId1,
                Name = "Alice",
                Surname = "Smith",
                Email = "alice.smith@example.com",
                PhoneNumber = "111222333",
                Password = "hashedpassword1"
            },
            new ClientUser
            {
                Id = ClientId2,
                Name = "Bob",
                Surname = "Johnson",
                Email = "bob.johnson@example.com",
                PhoneNumber = "444555666",
                Password = "hashedpassword2"
            }
        };

        _contextMock.Setup(d => d.ClientUsers).ReturnsDbSet(users);
    }

    [Fact]
    public void ModifyUser_UserModified_Success()
    {
        // Arrange
        var modifyDto = new ClientUserDto()
        {
            Id = ClientId1,
            Name = "UpdatedName",
            Surname = "UpdatedSurname",
            Email = "updated.email@example.com",
            PhoneNumber = "999888777"
        };

        // Act
        var result = _sut.ModifyUser(ClientId1, modifyDto);

        // Assert
        result!.Result.Item1.Should().Be(0);
        result!.Result.Item2.Should().Be("User updated");
    }

    [Fact]
    public async Task ModifyUser_UserNotExist_Failure()
    {
    // Arrange
        var nonExistentUserId = Guid.NewGuid();

        // Act
        var result = await _sut.ModifyUser(nonExistentUserId, new());

        // Assert
        result.Should().BeNull("because modifying a non-existent user should return null or equivalent failure result");
    }

    [Fact]
    public async Task ResetPassword_UserNotExist_Failure()
    {
        // Arrange
        var nonExistentUserId = Guid.NewGuid();

        // Act
        var result = await _sut.ResetUserPassword(nonExistentUserId);

        // Assert
        result.Should().BeNull("because resetting password for a non-existent user should return null or indicate failure");
    }

    [Fact]
    public async Task ResetPassword_WithConfirm_Success()
    {
        // Arrange
        var existingUser = _contextMock.Object.ClientUsers.First();
        var passwordName = "test123456";

        // Act
        var result = await _sut.ConfirmUserPassword(existingUser.Id, passwordName);

        // Assert
        result.Should().NotBeNull("because resetting password for an existing user should succeed");
        result!.Item1.Should().Be(200);
        result.Item2.Should().Be("Password confirmed.");
    }
    
}