// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:10:59 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DepartmentDisplayModelService : ServiceBase, IDepartmentDisplayModelService
    {
        protected IDepartmentService _service
        {
            get { return DependencyInjection.Container.Resolve<IDepartmentService>(); }
        }

        #region IDisplayModelService<DepartmentDisplayModel>

        public int GetCount()
        {
            return _service.GetCount();
        }

        public IList<DepartmentDisplayModel> GetAll()
        {
            var list = Worker.GetRepository<Department>().Table
                                        .Include(x => x.Users)
                                        .OrderBy(x => x.Name)
                                        .ToList();
            return BuildModels(list);
        }

        public IList<DepartmentDisplayModel> GetPage(int pageIndex, int pageSize)
        {
            var list = Worker.GetRepository<Department>().Table
                                        .Include(x => x.Users)
                                        .OrderByDescending(x => x.Id)
                                        .Skip(pageIndex * pageSize)
                                        .Take(pageSize)
                                        .ToList();
            return BuildModels(list);
        }

        public void Delete(DepartmentDisplayModel obj)
        {
            _service.DeleteById(GetId(obj));
        }

        public long GetId(DepartmentDisplayModel obj)
        {
            return obj.Target.Id;
        }

        #endregion

        private DepartmentDisplayModel BuildModel(Department item)
        {
            if (item == null)
            {
                return null;
            }

            return new DepartmentDisplayModel()
            {
                Target = item
            };
        }

        private IList<DepartmentDisplayModel> BuildModels(IList<Department> items)
        {
            if (items == null)
            {
                return null;
            }
            var list = new List<DepartmentDisplayModel>();
            foreach (var obj in items)
            {
                list.Add(BuildModel(obj));
            }
            return list;
        }
    }
}