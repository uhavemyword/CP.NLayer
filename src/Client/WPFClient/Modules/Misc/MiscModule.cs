namespace CP.NLayer.Client.WpfClient.Modules.Misc
{
    using CP.NLayer.Client.WpfClient.Common;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;

    [ModuleDependency("Tools")] //just for controling module order
    [Module(ModuleName = "Misc")]
    public class MiscModule : ModuleBase
    {
        protected override void RegisterTypes()
        {
            Container.RegisterType<object, About.View>(typeof(About.View).FullName);
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View));
        }
    }
}