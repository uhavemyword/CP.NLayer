// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public static class Extensions
    {
        /// <summary>
        /// Wrap the Include method(specify eager loading) of EntityFramework, so the upper layer needn't to reference to EntityFramework
        /// Usage: http://msdn.microsoft.com/en-us/library/gg671236%28v=vs.103%29.aspx
        /// </summary>
        /// <returns>Queryable{T}</returns>
        public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path) where T : class
        {
            return QueryableExtensions.Include<T, TProperty>(source, path);
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> source, string path) where T : class
        {
            return QueryableExtensions.Include<T>(source, path);
        }

        public static IQueryable Include(this IQueryable source, string path)
        {
            return QueryableExtensions.Include(source, path);
        }
    }
}