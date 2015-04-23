namespace CP.NLayer.Service.Contracts
{
    using System.ServiceModel;

    [ServiceContract]
    public interface ISystemService
    {
        [OperationContract, ApplyDataContractResolver]
        void InitializeDatabase(bool force);
    }
}