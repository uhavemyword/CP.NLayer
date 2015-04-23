// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using System.Collections.Generic;

    //[ServiceContract] Comment out as the Permission is fixed, and no need to expose this service to client.
    public interface IPermissionService : IEntityService<Permission>
    {
        //[OperationContract, ApplyDataContractResolver]
        IList<CheckListItem> GetCheckList();
    }
}