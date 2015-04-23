using System;

namespace CP.NLayer.Web.Mvc4.Common
{
    public static class StringExtentions
    {
        public static string Truncate(this string s, int length = 25)
        {
            s = s ?? string.Empty;
            if (s.Length > length)
            {
                s = s.Substring(0, length) + "...";
            }

            return s;
        }

        public static string EnsureNonNull(this string s)
        {
            return s ?? string.Empty;
        }

        public static string SurroundBrackets(this string s)
        {
            s = s ?? string.Empty;
            return "(" + s + ")";
        }

        public static bool EqualsOp(this string s, Operation op)
        {
            return s.Equals(op.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}