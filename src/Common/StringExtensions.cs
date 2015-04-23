// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    public static class StringExtensions
    {
        public static string Truncate(this string s, int length = 30)
        {
            s = s ?? string.Empty;
            if (s.Length > length)
            {
                s = s.Substring(0, length) + "...";
            }

            return s;
        }

        /// <summary>
        /// Always return a non-null value.
        /// </summary>
        /// <returns>a non-null string</returns>
        public static string GetValue(this string s)
        {
            return s ?? string.Empty;
        }

        public static string SurroundBrackets(this string s)
        {
            s = s ?? string.Empty;
            return "(" + s + ")";
        }

        public static string AsSafeSql(this string str)
        {
            if (str == null)
            {
                return null;
            }

            var newStr = str.Replace("'", "''");
            return newStr;
        }
    }
}