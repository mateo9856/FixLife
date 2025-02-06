using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models.AiModels;
using FixLife.ClientApp.Sessions;

namespace FixLife.ClientApp.Infrastructure.AiServices
{
    public class PlanRecommendationService : IPlanRecommendationService
    {
        private readonly WebApiClient<FreeTimeRecommendation> _webApiClient;

        public PlanRecommendationService(WebApiClient<FreeTimeRecommendation> webApiClient)
        {
            _webApiClient = webApiClient;
        }

        public async Task<FreeTimeRecommendation> GetFreeTimeRecommendationAsync(int count = 0)
        {
            var freeTimeRecommendationUrl = $"AiClient/freetimeRecommendations/{count}";

            try
            {
                var result = await _webApiClient.CallServiceGetAsync($"{freeTimeRecommendationUrl}", token: UserSession.Token);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
