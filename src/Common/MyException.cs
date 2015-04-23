// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 6/2/2014 1:06:00 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    using System;

    public class MyException : Exception
    {
        public MyException()
            : base()
        {
        }

        public MyException(string message)
            : base(message)
        {
        }

        public MyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}