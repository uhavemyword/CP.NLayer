// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/4/2013 11:04:46 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Modules.MainHeader
{
    using CP.NLayer.Client.WpfClient.Common;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;

    [Module(ModuleName = "MainHeader")]
    public class MainHeaderModule : ModuleBase
    {
        protected override void RegisterTypes()
        {
            Container.RegisterType<ViewModel>();
            Container.RegisterType<Popup.ViewModel>();
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainHeaderRegion, typeof(View));
        }
    }
}