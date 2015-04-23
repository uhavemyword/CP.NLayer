namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using System.Linq;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConfigService : EntityService<Config>, IConfigService
    {
        #region Ctor

        public ConfigService()
            : this(null)
        {
        }

        public ConfigService(IUnitOfWork worker)
            : base(worker)
        {
        }

        #endregion

        public Config GetByCategoryAndName(string category, string name)
        {
            return this.Worker.GetRepository<Config>().Table
                                    .Where(x => x.Category == category && x.Name == name)
                                    .FirstOrDefault();
        }

        public string GetValue(string category, string name)
        {
            var config = GetByCategoryAndName(category, name);
            return config != null ? (config.Value ?? string.Empty) : null;
        }

        public void SetValue(string category, string name, string value)
        {
            var config = GetByCategoryAndName(category, name);
            if (config != null)
            {
                config.Value = value;
                Update(config);
            }
            else
            {
                config = new Config()
                {
                    Category = category,
                    Name = name,
                    Value = value
                };
                Insert(config);
            }
        }
    }
}