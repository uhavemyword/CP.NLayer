using System;
using System.Resources;

namespace CP.NLayer.Web.Mvc4.Common
{
    public class Localizer
    {
        public static string GetString(string name, Type resourceType)
        {
            var rm = new ResourceManager(resourceType);
            rm.IgnoreCase = true;
            return rm.GetString(name);
        }
    }
}