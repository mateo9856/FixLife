using FixLife.Admin.Db.Entities;
using FixLife.Admin.Users.Abstraction;
using Moq.EntityFrameworkCore;

namespace FixLife.Admin.UnitTests.Users;

public class UsersTests : TestBase
{
    private readonly IClientUserService _clientUserService;

    public UsersTests(IClientUserService clientUserService)
    {
        _clientUserService = clientUserService;
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
    }
}