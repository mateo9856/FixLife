namespace FixLife.AI.Client.Models
{
    internal class GenerationConfig
    {
        public int Temperature { get; set; }
        public double TopK { get; set; }
        public double TopP { get; set; }
        public int MaxTokens { get; set; }
        public string ResponseMimeType { get; set; }
    }
}
