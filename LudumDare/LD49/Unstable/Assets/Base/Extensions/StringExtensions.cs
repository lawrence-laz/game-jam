using System.Linq;

namespace Libs.Base.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string text, params string[] searches)
        {
            return searches.Any(search => text.Contains(search));
        }

        public static bool ContainsAll(this string text, params string[] searches)
        {
            return searches.All(search => text.Contains(search));
        }
    }
}