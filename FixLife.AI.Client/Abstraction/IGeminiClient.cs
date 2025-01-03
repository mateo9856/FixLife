namespace FixLife.AI.Client.Abstraction
{
    public interface IGeminiClient
    {
        Task<string> CallPostPromptAsync(string promptJson);
    }
}
