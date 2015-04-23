// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Common;
    using CP.NLayer.Data;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System;

    public sealed class DependencyInjection
    {
        #region Singleton

        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() => new UnityContainer());

        static DependencyInjection()
        {
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            Container.RegisterType<IDepartmentService, DepartmentService>();
            Container.RegisterType<IPermissionService, PermissionService>();
            Container.RegisterType<IRoleService, RoleService>();
            Container.RegisterType<IUserService, UserService>();
            Container.RegisterType<ICrypto, Crypto>();
            //Container.RegisterType<IUserService, UserService>(new InjectionConstructor());
            //Container.RegisterType<IUserService, UserService>(new InjectionConstructor(new ResolvedParameter(typeof(IUnitOfWork))));
        }

        public static IUnityContainer Container
        {
            get { return _container.Value; }
        }

        #endregion
    }
}