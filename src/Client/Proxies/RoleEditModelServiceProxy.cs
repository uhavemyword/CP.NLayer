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

    public partial class RoleEditModelServiceProxy : ClientBase<IRoleEditModelService>, IRoleEditModelService
    {
        #region ctor
        public RoleEditModelServiceProxy()
        {
			this.Open();
        }

        public RoleEditModelServiceProxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
			this.Open();
        }

        public RoleEditModelServiceProxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public RoleEditModelServiceProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public RoleEditModelServiceProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
			this.Open();
        }
        #endregion

        #region IRoleEditModelService
        public RoleEditModel Create()
        {
            var result = Channel.Create();
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
            return result;
        }

        public RoleEditModel GetById(Int64 id)
        {
            var result = Channel.GetById(id);
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
            return result;
        }

        public RoleEditModel Insert(RoleEditModel obj)
        {
            var result = Channel.Insert(obj);
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
            return result;
        }

        public void Update(RoleEditModel obj)
        {
            Channel.Update(obj);
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

        public void Delete(RoleEditModel obj)
        {
            Channel.Delete(obj);
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

        public void DeleteById(Int64 id)
        {
            Channel.DeleteById(id);
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
