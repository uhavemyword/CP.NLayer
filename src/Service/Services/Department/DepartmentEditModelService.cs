// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:14:38 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DepartmentEditModelService : ServiceBase, IDepartmentEditModelService
    {
        protected IDepartmentService _service
        {
            get { return DependencyInjection.Container.Resolve<IDepartmentService>(); }
        }

        public DepartmentEditModel Create()
        {
            var item = new Department();
            return BuildModel(item);
        }

        public DepartmentEditModel GetById(long id)
        {
            var item = _service.GetById(id);
            return BuildModel(item);
        }

        public DepartmentEditModel Insert(DepartmentEditModel obj)
        {
            var item = _service.Insert(obj.Target);
            return BuildModel(item);
        }

        public void Update(DepartmentEditModel obj)
        {
            _service.Update(obj.Target);
        }

        public void Delete(DepartmentEditModel obj)
        {
            _service.Delete(obj.Target);
        }

        public void DeleteById(long id)
        {
            _service.DeleteById(id);
        }

        private DepartmentEditModel BuildModel(Department item)
        {
            if (item == null)
            {
                return null;
            }

            return new DepartmentEditModel()
            {
                Target = item
            };
        }
    }
}