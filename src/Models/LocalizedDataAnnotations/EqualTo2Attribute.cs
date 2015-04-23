// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.LocalizedDataAnnotations
{
    using CP.NLayer.Resources.Model;
    using DataAnnotationsExtensions;
    using System;

    /// <summary>
    /// Validates that the property has the same value as the given 'otherProperty'
    /// </summary>
    /// <remarks>
    /// From Mvc3 Futures
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EqualTo2Attribute : EqualToAttribute
    {
        public EqualTo2Attribute(string otherProperty)
            : base(otherProperty)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.EqualTo_Invalid;
            }

            var otherPropertyDisplayName = OtherPropertyDisplayName ?? OtherProperty;

            return string.Format(System.Globalization.CultureInfo.CurrentCulture, ErrorMessageString, name, otherPropertyDisplayName);
        }
    }
}