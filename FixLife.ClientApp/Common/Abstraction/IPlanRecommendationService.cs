using FixLife.ClientApp.Models.AiModels;

namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IPlanRecommendationService
    {
        Task<FreeTimeRecommendation> GetFreeTimeRecommendationAsync(int count = 0);
    }
}
