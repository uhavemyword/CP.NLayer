namespace CP.NLayer.Client.WpfClient.Modules.Tools
{
    using CP.NLayer.Client.WpfClient.Common;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;

    [ModuleDependency("Dashboard")] //just for controling module order
    [Module(ModuleName = "Tools")]
    public class ToolsModule : ModuleBase
    {
        protected override void RegisterTypes()
        {
            Container.RegisterType<object, Data.View>(typeof(Data.View).FullName);
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View));
        }
    }
}