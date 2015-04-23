// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/20/2013 3:02:28 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IEditModelService<T>
    {
        [OperationContract, ApplyDataContractResolver]
        T Create();

        [OperationContract, ApplyDataContractResolver]
        T GetById(long id);

        [OperationContract, ApplyDataContractResolver]
        T Insert(T obj);

        [OperationContract, ApplyDataContractResolver]
        void Update(T obj);

        [OperationContract, ApplyDataContractResolver]
        void Delete(T obj);

        [OperationContract, ApplyDataContractResolver]
        void DeleteById(long id);
    }
}