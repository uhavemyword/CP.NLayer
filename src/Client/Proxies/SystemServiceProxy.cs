// ------------------------------------------------------------------------------------
// This .cs file was auto-generated using t4 template. do not edit it directly - edit the associated .tt file instead.
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/26/2012 
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using CP.NLayer.Service.Contracts;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Models.Entities;

    public partial class SystemServiceProxy : ClientBase<ISystemService>, ISystemService
    {
        #region ctor
        public SystemServiceProxy()
        {
			this.Open();
        }

        public SystemServiceProxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
			this.Open();
        }

        public SystemServiceProxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public SystemServiceProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public SystemServiceProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
			this.Open();
        }
        #endregion

        #region ISystemService
        public void InitializeDatabase(bool force)
        {
            Channel.InitializeDatabase(force);
            try
            {
                if (this.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                this.Abort();
            }
        }

        #endregion
    }
}
