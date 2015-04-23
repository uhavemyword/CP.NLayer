// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DepartmentService : EntityService<Department>, IDepartmentService
    {
        #region ctor

        public DepartmentService()
            : this(null)
        {
        }

        public DepartmentService(IUnitOfWork worker)
            : base(worker)
        {
        }

        #endregion
    }
}