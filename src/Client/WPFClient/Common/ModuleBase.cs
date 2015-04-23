// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/7/2014 1:59:31 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public abstract class ModuleBase : IModule
    {
        protected IUnityContainer Container { get; private set; }

        protected IRegionManager RegionManager { get; private set; }

        public void Initialize()
        {
            this.Container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            this.RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            RegisterTypes();
            InitializeModule();
        }

        protected virtual void RegisterTypes()
        {
            // RequestNavigate can only find view instance in following registration way
            // Container.RegisterType<object, User.DisplayView>(typeof(User.DisplayView).FullName);
        }

        protected virtual void InitializeModule()
        {
        }
    }
}