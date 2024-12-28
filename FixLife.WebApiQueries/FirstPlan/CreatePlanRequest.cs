namespace FixLife.WebApiQueries.FirstPlan
{
    public class CreatePlanRequest
    {
        public WeeklyWorkDTO WeeklyWork { get; set; }
        public List<FreeTimeDTO> FreeTime { get; set; }
        public LearnTimeDTO LearnTime { get; set; }
    }
}
