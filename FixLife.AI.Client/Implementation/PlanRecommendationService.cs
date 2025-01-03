using FixLife.AI.Client.Abstraction;
using FixLife.AI.Client.Helpers;
using FixLife.AI.Client.Models;
using Newtonsoft.Json;
using System.Text;

namespace FixLife.AI.Client.Implementation
{
    public class PlanRecommendationService : IPlanRecomendationService
    {
        
        private readonly IGeminiClient _geminiClient;
        
        public PlanRecommendationService(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }

        public async Task<List<string>> GetWeeklyWork(int count = 0)
        {
            var promptText = ListOfJobsPrompt(count);
            var promptRequest = new PromptRequestSession();
            
            var promptToJson = JsonConvert.SerializeObject(promptRequest.CreatePlanRecommendationPrompt(promptText), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            var aiRequest = await _geminiClient.CallPostPromptAsync(promptToJson);

            var responseObj = JsonConvert.DeserializeObject<PromptResponse>(aiRequest);

            if (responseObj is null)
                return new List<string>();

            return PromptResponseHelper.GetFirstTextPart(responseObj).Split(',', StringSplitOptions.TrimEntries).ToList();

        }

        private string ListOfJobsPrompt(int count)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append("Generate me a list of random");

            if (count > 0)
            {
                messageBuilder.Append($" {count}");
            }
            messageBuilder.Append(" jobs, separated by comma.");
            return messageBuilder.ToString();
        }
    }
}