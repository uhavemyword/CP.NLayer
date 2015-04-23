// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 4/28/2014 10:40:04 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Telerik.Windows.Controls;

    public class AdornedElementToErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var element = value as AdornedElementPlaceholder;
            if (element.AdornedElement is RadNumericUpDown)
            {
                return "Please enter a valid number.";
            }
            var errors = element.DataContext as ReadOnlyObservableCollection<ValidationError>;
            var message = errors[0].ErrorContent.ToString();
            return message;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}