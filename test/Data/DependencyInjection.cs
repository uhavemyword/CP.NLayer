// ------------------------------------------------------------------------------------
//      Copyright (c) 2013 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 1/1/2013 11:06:10 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data.Tests
{
    using Microsoft.Practices.Unity;
    using System;

    internal sealed class DependencyInjection
    {
        #region Singleton

        private static readonly Lazy<UnityContainer> _container = new Lazy<UnityContainer>(() => new UnityContainer());

        static DependencyInjection()
        {
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
        }

        public static UnityContainer Container
        {
            get { return _container.Value; }
        }

        #endregion
    }
}