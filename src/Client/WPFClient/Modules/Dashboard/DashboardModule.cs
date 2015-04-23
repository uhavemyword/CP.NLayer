// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 7/17/2014 6:54:39 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard
{
    using CP.NLayer.Client.WpfClient.Common;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;

    [ModuleDependency("BasicData")] //just for controling module order
    [Module(ModuleName = "Dashboard")]
    public class DashboardModule : ModuleBase
    {
        protected override void RegisterTypes()
        {
            Container.RegisterType<object, Home.View>(typeof(Home.View).FullName);
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View));
        }
    }
}