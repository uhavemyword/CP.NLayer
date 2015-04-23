// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/20/2013 3:54:01 PM
// ------------------------------------------------------------------------------------

using CP.NLayer.Models;

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DisplayModelService<TDisplay, TEntity> : ServiceBase, IDisplayModelService<TDisplay>
        where TDisplay : DisplayModelBase<TEntity>, new()
        where TEntity : EntityBase, new()
    {
        protected IEntityService<TEntity> _service
        {
            get { return DependencyInjection.Container.Resolve<IEntityService<TEntity>>(); }
        }

        public virtual int GetCount()
        {
            return _service.GetCount();
        }

        public virtual IList<TDisplay> GetPage(int pageIndex, int pageSize)
        {
            var list = _service.GetPage(pageIndex, pageSize);
            return BuildModels(list);
        }

        public virtual void Delete(TDisplay obj)
        {
            _service.Delete(obj.Target);
        }

        public virtual long GetId(TDisplay obj)
        {
            return obj.Target.Id;
        }

        protected virtual TDisplay BuildModel(TEntity obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new TDisplay()
            {
                Target = obj
            };
        }

        protected virtual IList<TDisplay> BuildModels(IList<TEntity> objList)
        {
            if (objList == null)
            {
                return null;
            }
            var list = new List<TDisplay>();
            foreach (var obj in objList)
            {
                list.Add(BuildModel(obj));
            }
            return list;
        }

        public virtual IList<TDisplay> GetAll()
        {
            return BuildModels(_service.GetAll());
        }
    }
}