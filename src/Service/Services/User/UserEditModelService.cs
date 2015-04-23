// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/20/2013 4:18:13 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Common;
    using CP.NLayer.Data;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System;
    using System.Linq;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UserEditModelService : ServiceBase, IUserEditModelService
    {
        protected ICrypto _crypt
        {
            get { return DependencyInjection.Container.Resolve<ICrypto>(); }
        }

        protected IUserService _service
        {
            get { return DependencyInjection.Container.Resolve<IUserService>(); }
        }

        public UserEditModel Create()
        {
            var item = new User() { IsActive = true, PasswordHash = Guid.NewGuid().ToString("N") };
            return BuildModel(item);
        }

        public UserEditModel GetById(long id)
        {
            var item = Worker.GetRepository<User>().Table
                                            .Where(x => x.Id == id)
                                            .Include(x => x.Roles)
                                            .Include(x => x.Department)
                                            .FirstOrDefault();
            return BuildModel(item);
        }

        public UserEditModel Insert(UserEditModel obj)
        {
            obj.Target.DepartmentId = obj.Target.Department == null ? null : (int?)obj.Target.Department.Id;
            var user = Worker.GetRepository<User>().Add(obj.Target);
            if (!string.IsNullOrEmpty(obj.Password))
            {
                user.PasswordSalt = Guid.NewGuid().ToString("N");
                user.PasswordHash = _crypt.Encrypt(obj.Password, user.PasswordSalt);
            }

            var roles = Worker.GetRepository<Role>().Table.ToList();
            foreach (var role in roles)
            {
                if (obj.RoleList.Where(x => x.Text == role.Name && x.Checked == true).Count() == 1)
                {
                    user.Roles.Add(role);
                }
            }

            Worker.SaveChanges();
            return BuildModel(user);
        }

        public void Update(UserEditModel obj)
        {
            obj.Target.DepartmentId = obj.Target.Department == null ? null : (int?)obj.Target.Department.Id;
            var user = Worker.GetRepository<User>().Table.Where(x => x.Id == obj.Target.Id).Include(x => x.Roles).FirstOrDefault();
            if (!string.IsNullOrEmpty(obj.Password))
            {
                user.PasswordHash = _crypt.Encrypt(obj.Password, user.PasswordSalt);
            }

            var roles = Worker.GetRepository<Role>().Table.ToList();
            foreach (var role in roles)
            {
                if (obj.RoleList.Where(x => x.Text == role.Name && x.Checked == true).Count() == 1)
                {
                    if (!user.Roles.Any(x => x.Id == role.Id))
                    {
                        user.Roles.Add(role);
                    }
                }
                else
                {
                    if (user.Roles.Any(x => x.Id == role.Id))
                    {
                        user.Roles.Remove(role);
                    }
                }
            }

            Worker.GetRepository<User>().Update(user); // update relationship between user and role
            Worker.GetRepository<User>().Update(obj.Target);  // update flat fields
            Worker.SaveChanges();
        }

        public void Delete(UserEditModel entity)
        {
            var service = DependencyInjection.Container.Resolve<IUserService>();
            service.Delete(entity.Target);
        }

        public void DeleteById(long id)
        {
            var service = DependencyInjection.Container.Resolve<IUserService>();
            service.DeleteById(id);
        }

        private UserEditModel BuildModel(User item)
        {
            if (item == null)
            {
                return null;
            }

            var model = new UserEditModel();
            model.Target = item;
            var roleService = DependencyInjection.Container.Resolve<IRoleService>();
            model.RoleList = roleService.GetCheckList();
            model.RoleList.ToList().ForEach(x => x.Checked = model.Target.Roles.Where(y => y.Name == x.Text).Count() == 1);
            return model;
        }
    }
}