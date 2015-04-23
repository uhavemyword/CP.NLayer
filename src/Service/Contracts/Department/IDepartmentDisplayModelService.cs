// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:04:05 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using CP.NLayer.Models.Business;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDepartmentDisplayModelService : IDisplayModelService<DepartmentDisplayModel>
    {
    }
}