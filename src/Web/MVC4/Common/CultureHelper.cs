using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CP.NLayer.Web.Mvc4.Common
{
    public static class CultureHelper
    {
        public static List<CultureInfo> DefaultSupportedCultures
        {
            get
            {
                return new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("zh")
                };
            }
        }

        /// <summary>
        /// From supported culture list, find the one that closest match with currentCulture
        /// If it's not found, then return the first one in supported.
        /// </summary>
        public static CultureInfo GetSupportedCulture(CultureInfo currentCulture, List<CultureInfo> supportedCultures = null)
        {
            return GetSupportedCulture(currentCulture.Name, supportedCultures);
        }

        public static CultureInfo GetSupportedCulture(string code, List<CultureInfo> supportedCultures = null)
        {
            if (supportedCultures == null)
            {
                supportedCultures = DefaultSupportedCultures;
            }

            if (supportedCultures.Count() == 0)
            {
                throw new ArgumentException("supportedCultures is empty."); ;
            }

            foreach (var c in supportedCultures)
            {
                if (code == c.Name)
                {
                    return c;
                }
            }

            // If not find, find a close match. For example, if you have "en-US" defined and the user requests "en-GB",
            // the function will return closes match that is "en-US" because at least the language is the same (ie English)
            foreach (var c in supportedCultures)
            {
                if (code.ToLower().StartsWith(c.TwoLetterISOLanguageName))
                {
                    return c;
                }
            }

            // else return first supported culture
            return supportedCultures.First();
        }
    }
}