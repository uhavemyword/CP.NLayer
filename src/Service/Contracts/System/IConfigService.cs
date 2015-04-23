namespace CP.NLayer.Service.Contracts
{
    using CP.NLayer.Models.Entities;
    using System.ServiceModel;

    [ServiceContract]
    public interface IConfigService : IEntityService<Config>
    {
        [OperationContract, ApplyDataContractResolver]
        Config GetByCategoryAndName(string category, string name);

        [OperationContract, ApplyDataContractResolver]
        string GetValue(string category, string name);

        [OperationContract, ApplyDataContractResolver]
        void SetValue(string category, string name, string value);
    }
}