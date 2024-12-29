using Newtonsoft.Json;

namespace FixLife.AI.Client.Models
{
    internal class TextParts
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
