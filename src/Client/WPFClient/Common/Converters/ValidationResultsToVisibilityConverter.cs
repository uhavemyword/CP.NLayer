// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 4/28/2014 10:40:04 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ValidationResultsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new StringToVisibilityConverter().Convert(
                                    new ValidationResultsToStringConverter().Convert(value, targetType, parameter, culture),
                                    targetType,
                                    parameter,
                                    culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}