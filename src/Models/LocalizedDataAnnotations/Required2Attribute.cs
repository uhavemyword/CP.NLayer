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
    public class Required2Attribute : RequiredAttribute
    {
        public Required2Attribute()
            : base()
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.Required_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}