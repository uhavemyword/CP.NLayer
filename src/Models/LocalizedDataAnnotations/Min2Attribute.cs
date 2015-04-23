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
    public class Min2Attribute : MinAttribute
    {
        /// <summary>
        /// double
        /// </summary>
        public Min2Attribute(int min)
            : base(min)
        {
        }

        /// <summary>
        /// double
        /// </summary>
        public Min2Attribute(double min)
            : base(min)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.Min_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}