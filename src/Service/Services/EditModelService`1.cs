// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/20/2013 4:18:13 PM
// ------------------------------------------------------------------------------------

using CP.NLayer.Models;

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EditModelService<TEdit, TEntity> : ServiceBase, IEditModelService<TEdit>
        where TEdit : EditModelBase<TEntity>, new()
        where TEntity : EntityBase, new()
    {
        protected IEntityService<TEntity> _service
        {
            get { return DependencyInjection.Container.Resolve<IEntityService<TEntity>>(); }
        }

        public virtual TEdit Create()
        {
            return new TEdit() { Target = new TEntity() };
        }

        public virtual TEdit GetById(long id)
        {
            return BuildModel(_service.GetById(id));
        }

        public virtual TEdit Insert(TEdit obj)
        {
            return BuildModel(_service.Insert(obj.Target));
        }

        public virtual void Update(TEdit obj)
        {
            _service.Update(obj.Target);
        }

        public virtual void Delete(TEdit obj)
        {
            _service.Delete(obj.Target);
        }

        public virtual void DeleteById(long id)
        {
            _service.DeleteById(id);
        }

        private TEdit BuildModel(TEntity obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new TEdit()
            {
                Target = obj
            };
        }
    }
}