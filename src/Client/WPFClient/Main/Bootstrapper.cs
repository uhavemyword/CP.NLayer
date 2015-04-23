// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 1/1/2013 11:32:26 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Main
{
    using Common.Views;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.UnityExtensions;
    using Microsoft.Practices.Unity;
    using System.Windows;

    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject"/>, the
        /// <see cref="Bootstrapper"/> will attach the default <seealso cref="Microsoft.Practices.Composite.Regions.IRegionManager"/> of
        /// the application in its <see cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implemention ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures the <see cref="IUnityContainer"/>. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<object, MainBlankView>(typeof(MainBlankView).FullName);
            DependencyInjection.ConfigureContainer(this.Container);
            this.Container.RegisterInstance<IUnityContainer>(Container);
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //ModuleCatalog catalog = new ModuleCatalog();
            //catalog.AddModule(typeof(BasicDataModule));
            //catalog.AddModule(typeof(DashboardModule));
            //catalog.AddModule(typeof(ToolsModule));

            //return catalog;
            // Modules are copied to a directory as part of a post-build step.
            // These modules are not referenced in the project and are discovered by inspecting a directory.
            // Module projects have a post-build step to copy themselves into that directory.
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}