// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RoleService : EntityService<Role>, IRoleService
    {
        #region ctor

        public RoleService()
            : this(null)
        {
        }

        public RoleService(IUnitOfWork worker)
            : base(worker)
        {
        }

        #endregion

        #region IRoleService

        public IList<CheckListItem> GetCheckList()
        {
            var selectedList = new List<CheckListItem>();
            var roles = this.GetAll();
            foreach (var role in roles)
            {
                selectedList.Add(new CheckListItem
                {
                    Checked = false,
                    Value = null,
                    Text = role.Name,
                    Description = role.Description
                });
            }
            return selectedList;
        }

        #endregion
    }
}