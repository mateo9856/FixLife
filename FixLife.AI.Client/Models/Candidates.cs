using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class Candidates
    {
        [JsonProperty("content")]
        public Contents Content { get; set; }
        [JsonProperty("finishReason")]
        public string FinishReason { get; set; }
        [JsonProperty("avgLogprobs")]
        public double AvgLogprobs { get; set; }
    }
}
