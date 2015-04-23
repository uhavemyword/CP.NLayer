// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IRoleService : IEntityService<Role>
    {
        [OperationContract, ApplyDataContractResolver]
        IList<CheckListItem> GetCheckList();
    }
}