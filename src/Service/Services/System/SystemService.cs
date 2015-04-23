namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Service.Contracts;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SystemService : ISystemService
    {
        public void InitializeDatabase(bool force)
        {
            MyDbUtility.InitializeDatabase(force);
        }
    }
}