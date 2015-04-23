// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.LocalizedDataAnnotations
{
    using CP.NLayer.Resources.Model;
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Range2Attribute : RangeAttribute
    {
        public Range2Attribute(double minimum, double maximum)
            : base(minimum, maximum)
        {
        }

        public Range2Attribute(int minimum, int maximum)
            : base(minimum, maximum)
        {
        }

        public Range2Attribute(Type type, string minimum, string maximum)
            : base(type, minimum, maximum)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.Range_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}