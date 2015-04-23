// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 4/28/2014 10:40:04 PM
// ------------------------------------------------------------------------------------

using CP.NLayer.Common.License;

namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class EditionToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// convert the current edition to a availability of a menuItem
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">this parameter means which edition of this function belong to</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var currentEdition = (EditionEnum)value;
                var functionEdition = (EditionEnum)parameter;
                var result = currentEdition >= functionEdition;
                return result;
            }
            catch
            {
                throw new InvalidCastException(string.Format("Can not convert {0} to boolean!", value.GetType().FullName));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}