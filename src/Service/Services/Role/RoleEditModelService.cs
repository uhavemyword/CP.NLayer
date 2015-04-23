// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:14:38 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.Linq;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RoleEditModelService : ServiceBase, IRoleEditModelService
    {
        protected IRoleService _service
        {
            get { return DependencyInjection.Container.Resolve<IRoleService>(); }
        }

        public RoleEditModel Create()
        {
            var item = new Role();
            return BuildModel(item);
        }

        public RoleEditModel GetById(long id)
        {
            var item = Worker.GetRepository<Role>().Table.Where(x => x.Id == id).Include(x => x.Permissions).FirstOrDefault();
            return BuildModel(item);
        }

        public RoleEditModel Insert(RoleEditModel obj)
        {
            var role = Worker.GetRepository<Role>().Add(obj.Target);
            var permissions = Worker.GetRepository<Permission>().Table.ToList();
            foreach (var p in permissions)
            {
                if (obj.PermissionList.Where(x => x.Text == p.Name && x.Checked == true).Count() == 1)
                {
                    role.Permissions.Add(p);
                }
            }
            Worker.SaveChanges();
            return BuildModel(role);
        }

        //public RoleEditModel Insert(RoleEditModel obj)
        //{
        //    var worker = DependencyInjection.Container.Resolve<IUnitOfWork>(); // use same worker for following service
        //    var service = DependencyInjection.Container.Resolve<IRoleService>(new ParameterOverride("worker", worker));
        //    var permissionService = DependencyInjection.Container.Resolve<IPermissionService>(new ParameterOverride("worker", worker));
        //    var role = service.Insert(obj.Target);
        //    var permissions = permissionService.GetAll();
        //    foreach (var p in permissions)
        //    {
        //        if (obj.PermissionList.Where(x => x.Text == p.Name && x.Checked == true).Count() == 1)
        //        {
        //            role.Permissions.Add(p);
        //        }
        //    }
        //    service.Update(role);
        //    return new RoleEditModel
        //    {
        //        Item = role,
        //        PermissionList = service.GetCheckList()
        //    };
        //}

        public void Update(RoleEditModel obj)
        {
            var role = Worker.GetRepository<Role>().Table
                                            .Where(x => x.Id == (int)obj.Target.Id)
                                            .Include(x => x.Permissions)
                                            .FirstOrDefault();
            var permissions = Worker.GetRepository<Permission>().Table.ToList();
            foreach (var p in permissions)
            {
                if (obj.PermissionList.Where(x => x.Text == p.Name && x.Checked == true).Count() == 1)
                {
                    if (!role.Permissions.Any(x => x.Id == p.Id))
                    {
                        role.Permissions.Add(p);
                    }
                }
                else
                {
                    if (role.Permissions.Any(x => x.Id == p.Id))
                    {
                        role.Permissions.Remove(p);
                    }
                }
            }

            Worker.GetRepository<Role>().Update(role); // update relationship
            Worker.GetRepository<Role>().Update(obj.Target);  // update flat fields
            Worker.SaveChanges();
        }

        public void Delete(RoleEditModel obj)
        {
            _service.Delete(obj.Target);
        }

        public void DeleteById(long id)
        {
            _service.DeleteById(id);
        }

        private RoleEditModel BuildModel(Role item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new RoleEditModel();
            model.Target = item;
            var permissionService = DependencyInjection.Container.Resolve<IPermissionService>();
            model.PermissionList = permissionService.GetCheckList();
            model.PermissionList.ToList().ForEach(x => x.Checked = model.Target.Permissions.Where(y => y.Name == x.Text).Count() == 1);
            return model;
        }
    }
}