using Newtonsoft.Json;

namespace FixLife.ClientApp.Models
{
    [Serializable]
    public class AppPlan
    {
        [JsonProperty("WeeklyWork")]
        public WeeklyWork WeeklyWork { get; set; }
        [JsonProperty("FreeTime")]
        public List<FreeTime> FreeTime { get; set; }
        [JsonProperty("LearnTime")]
        public LearnTime LearnTime { get; set; }
    }
}
