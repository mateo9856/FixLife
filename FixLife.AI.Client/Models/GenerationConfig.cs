using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class GenerationConfig
    {
        [JsonProperty("temperature")]
        public int Temperature { get; set; }
        [JsonProperty("topK")]
        public double TopK { get; set; }
        [JsonProperty("topP")]
        public double TopP { get; set; }
        [JsonProperty("maxTokens")]
        public int MaxTokens { get; set; }
        [JsonProperty("responseMimeType")]
        public string ResponseMimeType { get; set; }
    }
}
