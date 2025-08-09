using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Entities.Plans;
using FixLife.Admin.Db.Enums;
using FixLife.Admin.Plans.Abstractions;
using FluentAssertions;
using Moq.EntityFrameworkCore;

namespace FixLife.Admin.UnitTests.Plan
{
    public class PlanTests : TestBase
    {
        private readonly IPlanService _sut;

        public PlanTests(IPlanService planService)
        {
            _sut = planService;
        }

        protected override void SetupEntity()
        {
            var plans = new List<ClientPlan>
            {
                new ClientPlan
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow.AddDays(-5),
                    WeeklyWork = new WeeklyWork
                    {
                        Id = Guid.NewGuid(),
                        TimeStart = new TimeSpan(8, 0, 0),
                        TimeEnd = new TimeSpan(16, 0, 0),
                        DayOfWeeks = new List<DayOfWeeks> { DayOfWeeks.Monday, DayOfWeeks.Wednesday }
                    },
                    LearnTime = new LearnTime
                    {
                        Id = Guid.NewGuid(),
                        TimeInterval = new TimeSpan(1, 0, 0),
                        StartTime = new TimeSpan(17, 0, 0),
                        DayOfWeeks = new List<DayOfWeeks> { DayOfWeeks.Tuesday }
                    },
                    FreeTime = new FreeTime
                    {
                        Id = Guid.NewGuid(),
                        TimeStart = new TimeSpan(18, 0, 0),
                        TimeEnd = new TimeSpan(20, 0, 0),
                        Text = "Reading"
                    }
                },
                new ClientPlan
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow.AddDays(-7),
                    UpdatedAt = DateTime.UtcNow.AddDays(-2),
                    WeeklyWork = new WeeklyWork
                    {
                        Id = Guid.NewGuid(),
                        TimeStart = new TimeSpan(9, 0, 0),
                        TimeEnd = new TimeSpan(17, 0, 0),
                        DayOfWeeks = new List<DayOfWeeks> { DayOfWeeks.Friday }
                    },
                    LearnTime = new LearnTime
                    {
                        Id = Guid.NewGuid(),
                        TimeInterval = new TimeSpan(2, 0, 0),
                        StartTime = new TimeSpan(19, 0, 0),
                        DayOfWeeks = new List<DayOfWeeks> { DayOfWeeks.Thursday }
                    },
                    FreeTime = new FreeTime
                    {
                        Id = Guid.NewGuid(),
                        TimeStart = new TimeSpan(20, 0, 0),
                        TimeEnd = new TimeSpan(22, 0, 0),
                        Text = "Gaming"
                    }
                },
                new ClientPlan
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    UpdatedAt = DateTime.UtcNow,
                    WeeklyWork = new WeeklyWork
                    {
                        Id = Guid.NewGuid(),
                        TimeStart = new TimeSpan(7, 30, 0),
                        TimeEnd = new TimeSpan(15, 30, 0),
                        DayOfWeeks = new List<DayOfWeeks> { DayOfWeeks.Sunday, DayOfWeeks.Saturday }
                    },
                    LearnTime = new LearnTime
                    {
                        Id = Guid.NewGuid(),
                        TimeInterval = new TimeSpan(1, 30, 0),
                        StartTime = new TimeSpan(16, 0, 0),
                        DayOfWeeks = new List<DayOfWeeks> { DayOfWeeks.Sunday }
                    },
                    FreeTime = new FreeTime
                    {
                        Id = Guid.NewGuid(),
                        TimeStart = new TimeSpan(21, 0, 0),
                        TimeEnd = new TimeSpan(23, 0, 0),
                        Text = "Movies"
                    }
                }
            };

            _contextMock.Setup(d => d.ClientPlans).ReturnsDbSet(plans);
        }

        [Fact]
        public void ModifyPlan_PlanModified_Success()
        {
            // Arrange
            var existingPlan = _contextMock.Object.ClientPlans.First();
            var modifyPlanDto = new 
            {
                Id = existingPlan.Id,
                Name = "UpdatedName",
                Description = "UpdatedDescription"
            };

            // Act
            var result = _sut.ModifyClientPlan(modifyPlanDto);

            // Assert
            result.Should().NotBeNull("because modifying an existing plan should succeed");
            result!.Id.Should().Be(existingPlan.Id);
        }

        [Fact]
        public void ModifyPlan_UserNotExist_Failure()
        {
            // Arrange
            var nonExistentPlanId = Guid.NewGuid();
            var modifyPlanDto = new Plans.Models.Plan()
            {
                Id = nonExistentPlanId,
                Name = "NonExistentPlan",
                Description = "NonExistentDescription"
            };

            // Act
            var result = _sut.ModifyClientPlan(modifyPlanDto);

            // Assert
            result.Should().BeNull("because modifying a non-existent plan should return null or indicate failure");
        }

        [Fact]
        public void DeletePlan_PlanDeleted_Success()
        {
            // Arrange
            var existingPlan = _contextMock.Object.ClientPlans.First();
            // Act
            var result = _sut.DeletePlan(Guid.NewGuid(), existingPlan.Id);

            // Assert
            result.Should().NotBeNull("because deleting an existing plan should succeed");
        }
    }
}
