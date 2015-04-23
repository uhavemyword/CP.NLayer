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
    public class Url2Attribute : UrlAttribute
    {
        private readonly UrlOptions _urlOptions = UrlOptions.RequireProtocol;

        /// <summary>
        /// Url, Default to require protocol
        /// </summary>
        public Url2Attribute()
            : base()
        {
        }

        public Url2Attribute(UrlOptions urlOptions)
            : base(urlOptions)
        {
            _urlOptions = urlOptions;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = _urlOptions == UrlOptions.RequireProtocol ? MResources.Url_Invalid : MResources.Url_WithoutProtocolRequired_Invalid;
            }

            return base.FormatErrorMessage(name);
        }
    }
}