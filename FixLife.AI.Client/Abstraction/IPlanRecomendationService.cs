namespace FixLife.AI.Client.Abstraction
{
    public interface IPlanRecomendationService
    {
        Task<List<string>> GetWeeklyWork(int count = 0);
    }
}
