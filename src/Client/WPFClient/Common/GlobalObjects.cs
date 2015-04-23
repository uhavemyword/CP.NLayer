// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 1/3/2013 4:23:00 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Common.License;
    using CP.NLayer.Models.Entities;

    public static class GlobalObjects
    {
        public static User CurrentUser { get; set; }

        //Comment out, use telerik implicit theme instead
        //private static Theme _currentTheme;

        //// http://www.telerik.com/help/wpf/3aafd588-ead6-4805-9778-1147d7014150.html
        //public static Theme CurrentTheme
        //{
        //    get
        //    {
        //        if (_currentTheme == null)
        //        {
        //            _currentTheme = new Windows8Theme();
        //        }
        //        return _currentTheme;
        //    }
        //    set
        //    {
        //        _currentTheme = value;
        //        StyleManager.ApplicationTheme = _currentTheme;
        //    }
        //}

        public static ProductKey ProductKey { get; set; }
    }
}