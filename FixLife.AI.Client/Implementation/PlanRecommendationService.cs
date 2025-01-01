using FixLife.AI.Client.Abstraction;
using FixLife.AI.Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FixLife.AI.Client.Implementation
{
    public class PlanRecommendationService : IPlanRecomendationService
    {
        private HttpClient _client;

        private readonly string GeminiAddress;

        public PlanRecommendationService()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(60);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GeminiAddress = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={Environment.GetEnvironmentVariable("GEMINI_API_KEY")}";
        }

        public async Task<List<string>> GetWeeklyWork(int count = 0)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Generate me a list of random");
            
            if (count > 0)
            {
                messageBuilder.Append($" {count}");
            }

            messageBuilder.Append(" jobs, separated by comma.");
            Uri uri = new Uri(GeminiAddress);

            var prompt = new PromptContent
            {
                Contents = new List<Contents>
                {
                    new Contents
                    {
                        Role = "user",
                        Parts = new List<TextParts>
                        {
                            new TextParts
                            {
                                Text = messageBuilder.ToString()
                            }
                        }
                    }
                }
            };


            try
            {
                var promptToJson = JsonConvert.SerializeObject(prompt, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

                var message = new HttpRequestMessage(HttpMethod.Post, uri);
                message.Content = new StringContent(promptToJson, Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(message);
                var result = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<PromptResponse>(result);

                return new List<string>();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }

        }
    }
}