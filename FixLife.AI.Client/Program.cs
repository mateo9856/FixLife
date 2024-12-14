using FixLife.AI.Client.Implementation;

namespace FixLife.AI.Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("OpenAI process test.");
            var service = new PlanRecommendationService();
            var result = await service.GetWeeklyWork();
            foreach(var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
