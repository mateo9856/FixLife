namespace FixLife.AI.Client
{
    public static class StringExtensions
    {
        public static string[] ToFirstLetterUpper(this string[] arr)
        {
            Span<char> destination = stackalloc char[1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].AsSpan(0, 1).ToUpperInvariant(destination);
                arr[i] = $"{destination}{arr[i].AsSpan(1)}";
            }
            return arr;
        }
    }
}
