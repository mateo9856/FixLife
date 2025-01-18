using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class Contents
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("parts")]
        public List<TextParts> Parts { get; set; }
    }
}
