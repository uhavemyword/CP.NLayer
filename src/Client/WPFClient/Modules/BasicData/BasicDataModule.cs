// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/7/2014 1:54:39 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Modules.BasicData
{
    using CP.NLayer.Client.WpfClient.Common;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;

    [ModuleDependency("MainHeader")] //just for controling module order
    [Module(ModuleName = "BasicData")]
    public class BasicDataModule : ModuleBase
    {
        protected override void RegisterTypes()
        {
            Container.RegisterType<object, Department.DisplayView>(typeof(Department.DisplayView).FullName);
            Container.RegisterType<object, Department.EditView>(typeof(Department.EditView).FullName);

            Container.RegisterType<object, Role.DisplayView>(typeof(Role.DisplayView).FullName);
            Container.RegisterType<object, Role.EditView>(typeof(Role.EditView).FullName);

            Container.RegisterType<object, User.DisplayView>(typeof(User.DisplayView).FullName);
            Container.RegisterType<object, User.EditView>(typeof(User.EditView).FullName);
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(View));
        }
    }
}