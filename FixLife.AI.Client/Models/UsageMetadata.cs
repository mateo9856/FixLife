using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class UsageMetadata
    {
        [JsonProperty("promptTokenCount")]
        public int PromptTokenCount { get; set; }
        [JsonProperty("candidatesTokenCount")]
        public int CandidatesTokenCount { get; set; }
        [JsonProperty("totalTokenCount")]
        public int TotalTokenCount { get; set; }
    }
}