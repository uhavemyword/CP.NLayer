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
    public class FileExtensions2Attribute : FileExtensionsAttribute
    {
        /// <summary>
        /// Provide the allowed file extensions, seperated via "|" (or a comma, ","), defaults to "png|jpe?g|gif"
        /// </summary>
        public FileExtensions2Attribute(string allowedExtensions = "png,jpg,jpeg,gif")
            : base(allowedExtensions)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = MResources.FileExtensions_Invalid;
            }
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, ErrorMessageString, name, Extensions);
        }
    }
}