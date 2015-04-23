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
    public class Integer2Attribute : IntegerAttribute
    {
        /// <summary>
        ///  Integer
        /// </summary>
        public Integer2Attribute()
            : base()
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.Integer_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}