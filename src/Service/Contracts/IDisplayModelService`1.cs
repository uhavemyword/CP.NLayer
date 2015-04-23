// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/20/2013 2:51:12 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDisplayModelService<T>
    {
        [OperationContract, ApplyDataContractResolver]
        int GetCount();

        [OperationContract, ApplyDataContractResolver]
        IList<T> GetAll();

        [OperationContract, ApplyDataContractResolver]
        IList<T> GetPage(int pageIndex, int pageSize);

        [OperationContract, ApplyDataContractResolver]
        void Delete(T obj);

        [OperationContract, ApplyDataContractResolver]
        long GetId(T obj);
    }
}