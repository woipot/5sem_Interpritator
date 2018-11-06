using System.Text.RegularExpressions;

namespace Interpritator.Source.Extension
{
    public static class StringExtension
    {
        public static string ReplaceOnlyWords(this string source, string oldWord, string newWord)
        {
            var pattern = $"\\b{oldWord}\\b";

            return Regex.Replace(source, pattern, newWord);

        }
    }
}
