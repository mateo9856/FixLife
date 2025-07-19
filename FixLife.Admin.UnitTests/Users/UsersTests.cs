using FixLife.Admin.Db.Entities;
using FixLife.Admin.Users.Abstraction;
using FixLife.Admin.Users.Exceptions;
using FixLife.Admin.Users.Implementation;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace FixLife.Admin.UnitTests.Users;

public class UsersTests : TestBase
{
    // Remove injected service field

    public UsersTests()
    {
        // Prepare mocked data for base setup
        SetupEntity();
    }

    protected override void SetupEntity()
    {
        var users = new List<ClientUser>
        {
            new ClientUser
            {
                Id = Guid.NewGuid(),
                Name = "Alice",
                Surname = "Smith",
                Email = "alice.smith@example.com",
                PhoneNumber = "111222333",
                Password = "hashedpassword1"
            },
            new ClientUser
            {
                Id = Guid.NewGuid(),
                Name = "Bob",
                Surname = "Johnson",
                Email = "bob.johnson@example.com",
                PhoneNumber = "444555666",
                Password = "hashedpassword2"
            }
        };

        _contextMock.Setup(d => d.ClientUsers).ReturnsDbSet(users);
        _contextMock.Setup(d => d.Set<ClientUser>()).ReturnsDbSet(users); // Support generic Set<T>() calls
    }

    private static ClientUserService CreateService(Mock<Admin.Db.Context.AdminContext> contextMock)
    {
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["ClientApiUri"]).Returns("http://localhost");

        var httpClientService = new HttpClientService(new HttpClient(), configMock.Object);

        return new ClientUserService(contextMock.Object, configMock.Object, httpClientService);
    }

    [Fact]
    public async Task ResetUserPassword_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new ClientUser
        {
            Id = userId,
            Name = "Test",
            Surname = "User",
            Email = "test.user@example.com",
            PhoneNumber = "999888777",
            Password = "oldpassword"
        };

        var users = new List<ClientUser> { user };
        _contextMock.Setup(d => d.ClientUsers).ReturnsDbSet(users);
        _contextMock.Setup(d => d.Set<ClientUser>()).ReturnsDbSet(users);

        var service = CreateService(_contextMock);

        // Act
        var (statusCode, message) = await service.ResetUserPassword(userId);

        // Assert
        statusCode.Should().Be(200);
        message.Should().Be("Redirect to page!");
    }

    [Fact]
    public async Task ResetUserPassword_ShouldThrowClientNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = new List<ClientUser>();
        _contextMock.Setup(d => d.ClientUsers).ReturnsDbSet(users);
        _contextMock.Setup(d => d.Set<ClientUser>()).ReturnsDbSet(users);

        var service = CreateService(_contextMock);

        // Act
        Func<Task> act = async () => await service.ResetUserPassword(userId);

        // Assert
        await act.Should().ThrowAsync<ClientNotFoundException>();
    }
}