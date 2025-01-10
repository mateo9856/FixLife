namespace FixLife.AI.Client.Abstraction
{
    public interface IPlanRecomendationService
    {
        Task<List<string>> GetFreeTimes(int count = 0);
    }
}
