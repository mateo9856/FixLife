using FixLife.WebApiDomain.Enums;
using FixLife.WebApiDomain.Models;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Common.Constants;
using MongoDB.Bson;
using Moq;

namespace FixLife.ApiTest
{
    public class PlanTest
    {
        private IPlanService _planService;
        private readonly Mock<IPlanService> _planMock = new Mock<IPlanService>();
        private Mock<Plan> _firstPlan = new Mock<Plan>();
        private Mock<Plan> _secondPlan = new Mock<Plan>();
        private Mock<PlanModel> _firstPlanModel = new Mock<PlanModel>();
        private Mock<PlanModel> _secondPlanModel = new Mock<PlanModel>();
        private ObjectId UserGuid { get; set; } = ObjectId.GenerateNewId();

        public PlanTest()
        {
            _planService = _planMock.Object;
            SetUp();
        }

        public void SetUp()
        {
            var dayOfWeeks = new List<DayOfWeeks> {
                DayOfWeeks.Monday,
                DayOfWeeks.Tuesday,
                DayOfWeeks.Wednesday,
            };

            var firstWeeklyWork = new WeeklyWork
            {
                Id = ObjectId.GenerateNewId(),
                CreatedDate = DateTime.Now,
                TimeStart = new TimeSpan(8, 0, 0),
                TimeEnd = new TimeSpan(16, 0, 0),
                DayOfWeeks = dayOfWeeks
            };

            var secondWeeklyWork = new WeeklyWork
            {
                Id = ObjectId.GenerateNewId(),
                CreatedDate = DateTime.Now,
                TimeStart = new TimeSpan(9, 0, 0),
                TimeEnd = new TimeSpan(17, 0, 0),
                DayOfWeeks = dayOfWeeks
            };

            _firstPlan.Object.Id = UserGuid;
            _secondPlan.Object.Id = UserGuid;

            _firstPlanModel.Object.WeeklyWork = firstWeeklyWork;
            _secondPlanModel.Object.WeeklyWork = secondWeeklyWork;

        }

        [Fact]
        public void Dashboard_ShouldEditSuccesful()
        {
            _planMock.Setup(d => d.EditPlanAsync(_firstPlanModel.Object, _secondPlanModel.Object, UserGuid.ToString()))
            .ReturnsAsync((HttpCodes.Ok, "TEST"));

            _planService = _planMock.Object;

            var act = _planService.EditPlanAsync(_firstPlanModel.Object, _secondPlanModel.Object, UserGuid.ToString()).Result;

            Xunit.Assert.True(act.Item1 == HttpCodes.Ok);
        }
    }
}