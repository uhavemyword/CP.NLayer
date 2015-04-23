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
    public class Max2Attribute : MaxAttribute
    {
        /// <summary>
        /// double
        /// </summary>
        public Max2Attribute(int max)
            : base(max)
        {
        }

        /// <summary>
        /// double
        /// </summary>
        public Max2Attribute(double max)
            : base(max)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.Max_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}