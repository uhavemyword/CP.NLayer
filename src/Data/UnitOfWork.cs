// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Common;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Resources.UI;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// A worker who is responsable for transporting data between business service layer and database.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private fields

        private DbContext _context = new MyDbContext();
        private Dictionary<Type, object> _repositoryDic = new Dictionary<Type, object>();

        #endregion

        #region Ctor

        public UnitOfWork()
        {
        }

        #endregion

        #region IUnitOfWork

        public IRepository<T> GetRepository<T>()
            where T : EntityBase
        {
            if (!_repositoryDic.Keys.Contains(typeof(T)))
            {
                var repository = new Repository<T>(_context);
                try
                {
                    _repositoryDic.Add(typeof(T), repository);
                }
                catch (ArgumentException)
                {
                    // Do nothing. The Try logic is to make sure thread safe.
                }
            }

            return _repositoryDic[typeof(T)] as IRepository<T>;
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var uniqueError = ex.GetUniqueViolationInfo();
                if (uniqueError != null)
                {
                    throw new MyException(string.Format(UiResources.Error_DuplicateName_2, uniqueError.Item1, uniqueError.Item2));
                }

                if (ex.IsDeleteStatementConflictedWithReference())
                {
                    throw new MyException("The selected item cannot be deleted because it is in use.(referenced by other object.)");
                }

                throw;
            }
        }

        public void ExecuteSqlCommand(string sql, params object[] parameters)
        {
            _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        #endregion

        #region Dispose

        private bool _disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing && _context != null)
                {
                    // Dispose managed resources.
                    _context.Dispose();
                    _context = null;
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
            }

            this._disposed = true;
        }

        #endregion
    }
}