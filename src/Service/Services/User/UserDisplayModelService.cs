// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/20/2013 3:54:01 PM
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
    public class UserDisplayModelService : ServiceBase, IUserDisplayModelService
    {
        protected IUserService _service
        {
            get { return DependencyInjection.Container.Resolve<IUserService>(); }
        }

        #region IDisplayModelService<UserDisplayModel>

        public int GetCount()
        {
            return _service.GetCount();
        }

        public IList<UserDisplayModel> GetAll()
        {
            var list = Worker.GetRepository<User>().Table
                                        .Include(x => x.Roles)
                                        .Include(x => x.Department)
                                        .ToList();
            return BuildModels(list);
        }

        public IList<UserDisplayModel> GetPage(int pageIndex, int pageSize)
        {
            var list = Worker.GetRepository<User>().Table
                                        .OrderByDescending(x => x.Id)
                                        .Skip(pageIndex * pageSize)
                                        .Take(pageSize)
                                        .Include(x => x.Roles)
                                        .Include(x => x.Department)
                                        .ToList();
            return BuildModels(list);
        }

        public void Delete(UserDisplayModel obj)
        {
            _service.DeleteById(GetId(obj));
        }

        public long GetId(UserDisplayModel obj)
        {
            return obj.Id;
        }

        #endregion

        private UserDisplayModel BuildModel(User item)
        {
            if (item == null)
            {
                return null;
            }

            return new UserDisplayModel()
            {
                Id = item.Id,
                FullName = item.FullName,
                UserName = item.UserName,
                IsActive = item.IsActive,
                Department = item.Department == null ? string.Empty : item.Department.Name,
                RoleNames = string.Join(", ", item.Roles.Select(x => x.Name))
            };
        }

        private IList<UserDisplayModel> BuildModels(IList<User> items)
        {
            if (items == null)
            {
                return null;
            }
            var list = new List<UserDisplayModel>();
            foreach (var obj in items)
            {
                list.Add(BuildModel(obj));
            }
            return list;
        }
    }
}