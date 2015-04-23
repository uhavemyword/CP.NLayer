// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using CP.NLayer.Models.Entities;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IEntityService<T> where T : EntityBase
    {
        [OperationContract, ApplyDataContractResolver]
        IList<T> GetAll();

        [OperationContract, ApplyDataContractResolver]
        T GetById(long id);

        [OperationContract, ApplyDataContractResolver]
        int GetCount();

        [OperationContract, ApplyDataContractResolver]
        IList<T> GetPage(int pageIndex, int pageSize);

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