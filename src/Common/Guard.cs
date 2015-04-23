// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    using System;
    using System.Linq.Expressions;

    public static class Guard
    {
        /// <summary>
        ///   Throws ArgumentNullException if any property of the object returned by newObjectExpression is null.
        ///   Usage: ThrowIfNull(() => new { parameter1, parameter2, ... });
        /// </summary>
        /// <param name="newObjectExpression"> A function returning an anonymously-typed object containing the parameters to verify. </param>
        public static void ThrowIfNull<T>(Expression<Func<T>> newObjectExpression)
        {
            var newExpression = newObjectExpression.Body as NewExpression;
            var obj = Expression.Lambda(newExpression).Compile().DynamicInvoke(null);
            var members = newExpression.Members;
            foreach (var member in members)
            {
                var value = obj.GetType().GetProperty(member.Name).GetValue(obj, null);
                if (value == null)
                {
                    throw new ArgumentNullException(member.Name);
                }
            }
        }
    }
}