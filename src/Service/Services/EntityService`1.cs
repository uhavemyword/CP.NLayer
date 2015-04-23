// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityService<T> : ServiceBase, IEntityService<T> where T : EntityBase
    {
        #region ctor

        public EntityService()
        {
        }

        public EntityService(IUnitOfWork worker)
        {
            this.Worker = worker;
        }

        #endregion ctor

        #region IEntityService

        public virtual IList<T> GetAll()
        {
            return Worker.GetRepository<T>().Table.OrderByDescending(x => x.Id).ToList();
        }

        public virtual T GetById(long id)
        {
            return Worker.GetRepository<T>().GetById(id);
        }

        public virtual int GetCount()
        {
            return Worker.GetRepository<T>().GetCount();
        }

        public virtual IList<T> GetPage(int pageIndex, int pageSize)
        {
            return Worker.GetRepository<T>().GetPage(pageIndex, pageSize);
        }

        public virtual T Insert(T entity)
        {
            var temp = Worker.GetRepository<T>().Add(entity);
            Worker.SaveChanges();
            return temp;
        }

        public virtual void Update(T entity)
        {
            Worker.GetRepository<T>().Update(entity);
            Worker.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Worker.GetRepository<T>().Remove(entity);
            Worker.SaveChanges();
        }

        public virtual void DeleteById(long id)
        {
            Worker.GetRepository<T>().RemoveById(id);
            Worker.SaveChanges();
        }

        #endregion IEntityService
    }
}