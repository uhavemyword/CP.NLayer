// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:10:59 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RoleDisplayModelService : ServiceBase, IRoleDisplayModelService
    {
        protected IRoleService _service
        {
            get { return DependencyInjection.Container.Resolve<IRoleService>(); }
        }

        #region IRoleDisplayModelService

        public int GetCount()
        {
            return _service.GetCount();
        }

        public IList<RoleDisplayModel> GetAll()
        {
            var list = _service.GetAll();
            return BuildModels(list);
        }

        public IList<RoleDisplayModel> GetPage(int pageIndex, int pageSize)
        {
            var list = _service.GetPage(pageIndex, pageSize);
            return BuildModels(list);
        }

        public void Delete(RoleDisplayModel obj)
        {
            _service.DeleteById(GetId(obj));
        }

        public long GetId(RoleDisplayModel obj)
        {
            return obj.Target.Id;
        }

        #endregion

        private RoleDisplayModel BuildModel(Role item)
        {
            if (item == null)
            {
                return null;
            }

            return new RoleDisplayModel()
            {
                Target = item
            };
        }

        private IList<RoleDisplayModel> BuildModels(IList<Role> items)
        {
            if (items == null)
            {
                return null;
            }
            var list = new List<RoleDisplayModel>();
            foreach (var obj in items)
            {
                list.Add(BuildModel(obj));
            }
            return list;
        }
    }
}