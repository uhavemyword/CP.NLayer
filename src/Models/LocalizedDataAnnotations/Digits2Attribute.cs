// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.LocalizedDataAnnotations
{
    using CP.NLayer.Resources.Model;
    using DataAnnotationsExtensions;
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Digits2Attribute : DigitsAttribute
    {
        /// <summary>
        /// long, and >=0
        /// </summary>
        public Digits2Attribute()
            : base()
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.Digits_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}