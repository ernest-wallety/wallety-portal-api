using Wallety.Portal.Core.Enum;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Core.Helpers
{
    public static class ExtensionMethods
    {
        public static string ReplaceSingleWithDoubleQuotes(this string input)
        {
            // Check if the input string is not null or empty
            if (string.IsNullOrEmpty(input))
                return input;

            // Check if there's at least one single quote in the input string
            if (input.Contains('\'')) return input.Replace("'", "\"");


            // Return the original input if no single quote is found
            return input;
        }

        public static string ToCommaSeparated(this int[]? arr)
        {
            if (arr == null || arr.Length == 0)
                return "";

            return string.Join(",", arr);
        }

        public static List<string> ToStringList(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return [];

            return [.. str
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())];
        }

        public static T WithDisplayData<T>(this T ex, EnumValidationDisplay display) where T : Exception
        {
            ex.Data["errorDisplay"] = display;
            return ex;
        }

        public static DataList<T> ToDataList<T>(this IEnumerable<T> source)
            where T : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var count = source.Count();
            var items = source.ToList(); // Convert to List for IReadOnlyList

            return new DataList<T>(count, items);
        }
    }
}
