// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 4/28/2014 10:40:04 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class ValidationResultsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            try
            {
                var results = (IEnumerable<ValidationResult>)value;
                var errors = results.Where(x => x.MemberNames == null || x.MemberNames.Count() == 0) // exclude property error which already showed around the related control
                                        .Select(x => "● " + x.ErrorMessage).ToArray();
                return string.Join(Environment.NewLine, errors).TrimEnd();
            }
            catch
            {
                throw new InvalidCastException(string.Format("Can not convert {0} to string!", value.GetType().FullName));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}