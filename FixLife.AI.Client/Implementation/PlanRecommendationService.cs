using FixLife.AI.Client.Abstraction;
using OpenAI;
using OpenAI.Chat;
using System.Diagnostics.Contracts;
using System.Text;

namespace FixLife.AI.Client.Implementation
{
    public class PlanRecommendationService : IPlanRecomendationService
    {
        private ChatClient _chatClient;
        
        public PlanRecommendationService()
        {
            _chatClient = new("gpt-4o-mini", Environment.GetEnvironmentVariable("OPEN_AI_KEY"));    
        }
        
        public async Task<List<string>> GetWeeklyWork(int count = 0)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Generate me a list of random");
            if(count > 0)
            {
                messageBuilder.Append($" {count}");
            }
            messageBuilder.Append(" jobs, separated by comma.");

            var chatCompletion = await _chatClient.CompleteChatAsync(messageBuilder.ToString());

            await Task.Delay(500);

            var responseText = chatCompletion.Value.Content[0].Text;

            return responseText
                .Remove(responseText.Length - 1)
                .Split(',', StringSplitOptions.TrimEntries)
                .ToList();
        }

    }
}
