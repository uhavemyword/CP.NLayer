// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/29/2012 11:07:01 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Data;
    using Microsoft.Practices.Unity;

    public abstract class ServiceBase
    {
        private IUnitOfWork _worker;

        protected IUnitOfWork Worker
        {
            get
            {
                if (_worker == null)
                {
                    _worker = DependencyInjection.Container.Resolve<IUnitOfWork>();
                }
                return _worker;
            }
            set
            {
                _worker = value;
            }
        }
    }
}