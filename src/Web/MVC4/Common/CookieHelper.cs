using System;
using System.Web;

namespace CP.NLayer.Web.Mvc4.Common
{
    public class CookieHelper
    {
        /// <summary>
        /// Get the value(decrypt) of cookie by specific key. Return empty string if the cookie doesn't exist
        /// </summary>
        public static string Get(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

            //return (cookie == null) ? string.Empty : Encoding.UTF8.GetString(Convert.FromBase64String(cookie.Value));
            return (cookie == null) ? string.Empty : cookie.Value;
        }

        public static void Set(string key, string value)
        {
            var dayExpires = 30;
            Set(key, value, dayExpires);
        }

        /// <summary>
        /// Add or update a cookie
        /// </summary>
        public static void Set(string key, string value, int dayExpires)
        {
            //TODO: Encrypt cookie
            //value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

            if (cookie == null)
            {
                cookie = new HttpCookie(key) { Value = value, Expires = DateTime.UtcNow.AddDays(dayExpires) };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                cookie.Value = value;
                cookie.Expires = DateTime.UtcNow.AddDays(dayExpires);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        public static void Delete(string key)
        {
            Set(key, string.Empty, -1);
        }
    }
}