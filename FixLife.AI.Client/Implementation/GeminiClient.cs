using FixLife.AI.Client.Abstraction;
using System.Net.Http.Headers;
using System.Text;

namespace FixLife.AI.Client.Implementation
{
    internal class GeminiClient : IGeminiClient
    {
        private HttpClient _client;
        private readonly string GeminiAddress;

        public GeminiClient()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(60);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            GeminiAddress = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={Environment.GetEnvironmentVariable("GEMINI_API_KEY")}";
        }

        public async Task<string> CallPostPromptAsync(string promptJson)
        {
            try
            {
                Uri uri = new Uri(GeminiAddress);

                var message = new HttpRequestMessage(HttpMethod.Post, uri);
                message.Content = new StringContent(promptJson, Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(message);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Gemini", ex);
            }
        }
    }
}
