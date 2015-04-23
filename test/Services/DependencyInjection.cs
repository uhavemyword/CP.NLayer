// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/19/2013 10:38:43 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services.Tests
{
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
            container.RegisterType<IRoleService, RoleService>(new InjectionConstructor());
            container.RegisterType<IUserService, UserService>(new InjectionConstructor());
            container.RegisterType<ISystemService, SystemService>();
        }
    }
}