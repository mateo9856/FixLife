using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class PromptResponse
    {
        [JsonProperty("candidates")]
        public List<Candidates> Candidates { get; set; }
        [JsonProperty("usageMetadata")]
        public UsageMetadata UsageMetadata { get; set; }
        [JsonProperty("modelVersion")]
        public string ModelVersion { get; set; }
    }
}
