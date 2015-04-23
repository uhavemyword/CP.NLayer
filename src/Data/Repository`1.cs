// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    internal class Repository<T> : IRepository<T> where T : EntityBase
    {
        #region Private fields

        private DbContext _context;
        private DbSet<T> _dbSet;

        #endregion

        #region Ctor

        public Repository(DbContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<T>();
        }

        #endregion

        #region IRepository members

        public IQueryable<T> Table
        {
            get { return _dbSet; }
        }

        public T GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public int GetCount()
        {
            return _dbSet.Count();
        }

        public IList<T> GetPage(int pageIndex, int pageSize)
        {
            return _dbSet.OrderByDescending(x => x.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public T Add(T entity)
        {
            if (!IsProxyType(entity))
            {
                var temp = _dbSet.Create();
                _dbSet.Add(temp);
                _context.Entry(temp).CurrentValues.SetValues(entity);
                return temp;
            }
            return _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var entry = _context.Entry<T>(entity);
            if (entry.State == EntityState.Detached)
            {
                var attachedEntity = _dbSet.Find(entity.Id);
                if (attachedEntity == null || !attachedEntity.RowVersion.SequenceEqual(entity.RowVersion))
                {
                    throw new DBConcurrencyException("Entities may have been modified or deleted since entities were loaded. Refresh ObjectStateManager entries.");
                }
                _context.Entry(attachedEntity).CurrentValues.SetValues(entity);
                entity = attachedEntity;
                return;
            }
            entry.State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            if (entity != null)
            {
                var entry = _context.Entry<T>(entity);
                if (entry.State == EntityState.Detached)
                {
                    var attachedEntity = _dbSet.Find(entity.Id);
                    if (attachedEntity != null)
                    {
                        _dbSet.Remove(attachedEntity);
                        return;
                    }
                }
                _dbSet.Remove(entity);
            }
        }

        public void RemoveById(long id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public T GetWithRelations(T entity, params Expression<Func<T, object>>[] includePaths)
        {
            var id = entity.Id;
            var query = _dbSet.Where(x => x.Id == id);
            foreach (var path in includePaths)
            {
                query = query.Include(path);
            }
            return query.FirstOrDefault();
        }

        public IList<T> GetWithRelations(IList<T> collection, params Expression<Func<T, object>>[] includePaths)
        {
            var idList = collection.Select(x => x.Id).ToList();
            var query = _dbSet.Where(x => idList.Contains(x.Id));
            foreach (var path in includePaths)
            {
                query = query.Include(path);
            }
            return query.ToList();
        }

        private bool IsProxyType(object obj)
        {
            return obj != null && System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(obj.GetType()) != obj.GetType();
        }

        #endregion
    }
}