using AutoFixture;
using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace FixLife.Admin.UnitTests
{
    public abstract class TestBase
    {
        protected Mock<AdminContext> _contextMock;

        protected IFixture _fixture;

        public TestBase()
        {
            _fixture = new Fixture();
            SetupDbContext();
            PrepareBaseRecords();
        }

        protected void SetupDbContext()
        {
            _contextMock = new Mock<AdminContext>(new DbContextOptions<AdminContext>());
        }

        protected abstract void SetupEntity();

        private void PrepareBaseRecords()
        {
            var listUsers = new List<AdminUser>();

            for (int i = 0; i < 5; i++)
                listUsers.Add(_fixture.Create<AdminUser>());

            _contextMock.Setup(d => d.Users).ReturnsDbSet(listUsers);
        }

    }
}