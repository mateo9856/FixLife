using FixLife.AI.Client.Models;

namespace FixLife.AI.Client.Helpers
{
    internal class PromptRequestSession
    {
        private PromptContent _content;

        internal PromptContent CreatePlanRecommendationPrompt(string promptText)
        {
            return new PromptContent
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
                                Text = promptText
                            }
                        }
                    }
                }
            };
        }
    }
}
