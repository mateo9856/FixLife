using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class PromptContent
    {
        [JsonProperty("contents")]
        public List<Contents> Contents { get; set; }
        [JsonProperty("generationConfig")]
        public GenerationConfig GenerationConfig { get; set; }
    }
}
