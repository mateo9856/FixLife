using FixLife.WebApiDomain.Enums;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using Moq;
using NUnit.Framework;
using DayOfWeekObject= FixLife.WebApiDomain.Common.DayOfWeek;

namespace FixLife.ApiTest
{
    public class PlanTest
    {
        private IPlanService _planService;
        private readonly Mock<IPlanService> _planMock = new Mock<IPlanService>();
        private Mock<Plan> _firstPlan = new Mock<Plan>();
        private Mock<Plan> _secondPlan = new Mock<Plan>();
        private Guid UserGuid { get; set; } = Guid.NewGuid();

        public PlanTest()
        {
            _planService = _planMock.Object;
            SetUp();
        }

        public void SetUp()
        {
            var dayOfWeeks = new List<DayOfWeekObject> { 
                new DayOfWeekObject { Day = DayOfWeeks.Monday },
                new DayOfWeekObject { Day = DayOfWeeks.Tuesday },
                new DayOfWeekObject { Day = DayOfWeeks.Wednesday },
            };

            var firstWeeklyWork = new WeeklyWork
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                TimeStart = new TimeSpan(8, 0, 0),
                TimeEnd = new TimeSpan(16, 0, 0),
                DayOfWeeks = dayOfWeeks.ConvertAll(x => (FixLife.WebApiDomain.Common.DayOfWeek)x)
            };

            var secondWeeklyWork = new WeeklyWork
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                TimeStart = new TimeSpan(9, 0, 0),
                TimeEnd = new TimeSpan(17, 0, 0),
                DayOfWeeks = dayOfWeeks.ConvertAll(x => (FixLife.WebApiDomain.Common.DayOfWeek)x)
            };

            _firstPlan.Object.Id = UserGuid;
            _secondPlan.Object.Id = UserGuid;

            _firstPlan.Object.WeeklyWork = firstWeeklyWork;
            _secondPlan.Object.WeeklyWork = secondWeeklyWork;

        }

        [Fact]
        public void Dashboard_ShouldEditSuccesful()
        {
            _planMock.Setup(d => d.EditPlanAsync(_firstPlan.Object, _secondPlan.Object, UserGuid.ToString()))
            .ReturnsAsync(((short)200, "TEST"));

            _planService = _planMock.Object;

            var act = _planService.EditPlanAsync(_firstPlan.Object, _secondPlan.Object, UserGuid.ToString()).Result;

            Xunit.Assert.True(act.Item1 == 200);
        }
    }
}