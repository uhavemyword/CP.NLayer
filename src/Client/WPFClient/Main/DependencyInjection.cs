// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 1/27/2013 10:40:04 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Main
{
    using CP.NLayer.Client.Proxies;
    using CP.NLayer.Client.WpfClient.Common;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Service.Contracts;
    using CP.NLayer.Service.Services;
    using Microsoft.Practices.Unity;
    using System;

    public class DependencyInjection
    {
        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() => new UnityContainer());

        static DependencyInjection()
        {
            ConfigureContainer(Container);
        }

        public static IUnityContainer Container
        {
            get { return _container.Value; }
        }

        public static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IInteractionService, InteractionService>();

            bool useWcfService = System.Configuration.ConfigurationManager.AppSettings["UseWcfService"].ToLower() == "true";
            if (useWcfService)
            {
                // User
                container.RegisterType<IUserService, UserServiceProxy>(new InjectionConstructor("UserServiceEndPoint"));
                container.RegisterType<IUserDisplayModelService, UserDisplayModelServiceProxy>(new InjectionConstructor("UserDisplayModelServiceEndPoint"));
                container.RegisterType<IDisplayModelService<UserDisplayModel>, UserDisplayModelServiceProxy>(new InjectionConstructor("UserDisplayModelServiceEndPoint"));
                container.RegisterType<IUserEditModelService, UserEditModelServiceProxy>(new InjectionConstructor("UserEditModelServiceEndPoint"));
                container.RegisterType<IEditModelService<UserEditModel>, UserEditModelServiceProxy>(new InjectionConstructor("UserEditModelServiceEndPoint"));

                // Role
                container.RegisterType<IRoleService, RoleServiceProxy>(new InjectionConstructor("RoleServiceEndPoint"));
                container.RegisterType<IRoleDisplayModelService, RoleDisplayModelServiceProxy>(new InjectionConstructor("RoleDisplayModelServiceEndPoint"));
                container.RegisterType<IDisplayModelService<RoleDisplayModel>, RoleDisplayModelServiceProxy>(new InjectionConstructor("RoleDisplayModelServiceEndPoint"));
                container.RegisterType<IRoleEditModelService, RoleEditModelServiceProxy>(new InjectionConstructor("RoleEditModelServiceEndPoint"));
                container.RegisterType<IEditModelService<RoleEditModel>, RoleEditModelServiceProxy>(new InjectionConstructor("RoleEditModelServiceEndPoint"));

                // Department
                container.RegisterType<IDepartmentService, DepartmentServiceProxy>(new InjectionConstructor("DepartmentServiceEndPoint"));
                container.RegisterType<IDisplayModelService<DepartmentDisplayModel>, DepartmentDisplayModelServiceProxy>(new InjectionConstructor("DepartmentDisplayModelServiceEndPoint"));
                container.RegisterType<IEditModelService<DepartmentEditModel>, DepartmentEditModelServiceProxy>(new InjectionConstructor("DepartmentEditModelServiceEndPoint"));
                container.RegisterType<IDepartmentDisplayModelService, DepartmentDisplayModelServiceProxy>(new InjectionConstructor("DepartmentDisplayModelServiceEndPoint"));
                container.RegisterType<IDepartmentEditModelService, DepartmentEditModelServiceProxy>(new InjectionConstructor("DepartmentEditModelServiceEndPoint"));

                // System
                container.RegisterType<ISystemService, SystemServiceProxy>(new InjectionConstructor("SystemServiceEndPoint"));
                container.RegisterType<IConfigService, ConfigServiceProxy>(new InjectionConstructor("ConfigServiceEndPoint"));
            }
            else
            {
                // TODO: Use WcfService to remove the reference to CP.NLayer.Service.Services, service code should not deploy to client side.
                // User
                container.RegisterType<IUserService, UserService>(new InjectionConstructor());
                container.RegisterType<IUserDisplayModelService, UserDisplayModelService>();
                container.RegisterType<IDisplayModelService<UserDisplayModel>, UserDisplayModelService>();
                container.RegisterType<IUserEditModelService, UserEditModelService>();
                container.RegisterType<IEditModelService<UserEditModel>, UserEditModelService>();

                // Role
                container.RegisterType<IRoleService, RoleService>(new InjectionConstructor());
                container.RegisterType<IRoleDisplayModelService, RoleDisplayModelService>();
                container.RegisterType<IDisplayModelService<RoleDisplayModel>, RoleDisplayModelService>();
                container.RegisterType<IRoleEditModelService, RoleEditModelService>();
                container.RegisterType<IEditModelService<RoleEditModel>, RoleEditModelService>();

                // Department
                container.RegisterType<IDepartmentService, DepartmentService>(new InjectionConstructor());
                container.RegisterType<IDisplayModelService<DepartmentDisplayModel>, DepartmentDisplayModelService>();
                container.RegisterType<IEditModelService<DepartmentEditModel>, DepartmentEditModelService>();
                container.RegisterType<IDepartmentDisplayModelService, DepartmentDisplayModelService>();
                container.RegisterType<IDepartmentEditModelService, DepartmentEditModelService>();

                // System
                container.RegisterType<ISystemService, SystemService>();
                container.RegisterType<IConfigService, ConfigService>(new InjectionConstructor());
            }
        }
    }
}