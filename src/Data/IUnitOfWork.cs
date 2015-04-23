// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Models.Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : EntityBase;

        void SaveChanges();

        void ExecuteSqlCommand(string sql, params object[] parameters);
    }
}