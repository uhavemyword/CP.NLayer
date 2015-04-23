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
    public class PermissionService : EntityService<Permission>, IPermissionService
    {
        #region ctor

        public PermissionService()
            : this(null)
        {
        }

        public PermissionService(IUnitOfWork worker)
            : base(worker)
        {
        }

        #endregion

        #region IPermissionService

        public IList<CheckListItem> GetCheckList()
        {
            var selectedList = new List<CheckListItem>();
            var permissions = this.GetAll();
            foreach (var p in permissions)
            {
                selectedList.Add(new CheckListItem
                {
                    Checked = false,
                    Value = null,
                    Text = p.Name,
                    Description = p.Description
                });
            }
            return selectedList;
        }

        #endregion
    }
}