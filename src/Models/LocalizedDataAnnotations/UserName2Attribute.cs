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
    public class UserName2Attribute : RegularExpressionAttribute
    {
        /// <summary>
        /// match Regex (@"^[a-zA-Z0-9_]+$")
        /// </summary>
        public UserName2Attribute()
            : base(@"^[a-zA-Z0-9_]+$")
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.UserName_Invalid;
            }
            return base.FormatErrorMessage(name);
        }
    }
}