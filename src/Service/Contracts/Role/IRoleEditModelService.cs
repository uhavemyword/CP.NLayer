// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:06:51 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;
    using System.ServiceModel;

    [ServiceContract]
    [ServiceKnownType(typeof(Permission))]
    public interface IRoleEditModelService : IEditModelService<RoleEditModel>
    {
    }
}