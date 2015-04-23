// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> Table { get; }

        T GetById(long id);

        int GetCount();

        IList<T> GetPage(int pageIndex, int pageSize);

        T Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        void RemoveById(long id);

        T GetWithRelations(T entity, params Expression<Func<T, object>>[] includePaths);

        IList<T> GetWithRelations(IList<T> collection, params Expression<Func<T, object>>[] includePaths);
    }
}