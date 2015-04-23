// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 3/1/2013 3:27:12 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    using CP.NLayer.Resources.UI;
    using System;

    public static class MiscExtensions
    {
        /// <summary>
        /// Get custom/localized error message for specific exception type.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>custom/localized error message if the exception is some know type, null otherwise.</returns>
        public static string CustomMessage(this Exception exception)
        {
            if (exception.IsUniqueConstraintViolation())
            {
                return UiResources.UniqueConstraintViolationError;
            }
            else if (exception.IsDbConcurrencyException())
            {
                return UiResources.DbConcurrencyError;
            }
            else
            {
                return null;
            }
        }
    }
}