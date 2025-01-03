using FixLife.AI.Client.Models;

namespace FixLife.AI.Client.Helpers
{
    internal static class PromptResponseHelper
    {
        public static string GetFirstTextPart(PromptResponse response)
        {
            var textParts = string.Empty;
            if (response.Candidates != null && response.Candidates.Count > 0)
            {
                var candidate = response.Candidates[0];
                if (candidate.Content != null && candidate.Content.Parts != null)
                {
                    textParts = candidate?.Content?.Parts[0]?.Text 
                        ?? string.Empty;
                }
            }
            return textParts;
        }
    }
}
