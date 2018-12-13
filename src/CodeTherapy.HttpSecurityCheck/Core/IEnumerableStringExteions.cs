using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeTherapy.HttpSecurityChecks.Core
{
    public static class IEnumerableStringExteions
    {
        public static bool AnyOrdinalIgnoreCase(this IEnumerable<string> enumerable, string value)
        {
            return enumerable.Any(source => string.Equals(source, value, StringComparison.OrdinalIgnoreCase));
        }

        public static bool EqualsOrdinalIgnoreCase(this string s, string value)
        {
            return string.Equals(s, value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool StartsWithOrdinalIgnoreCase(this string s, string value)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            return s.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

    }

}
