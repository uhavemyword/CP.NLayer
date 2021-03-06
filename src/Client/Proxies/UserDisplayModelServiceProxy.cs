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

    public partial class UserDisplayModelServiceProxy : ClientBase<IUserDisplayModelService>, IUserDisplayModelService
    {
        #region ctor
        public UserDisplayModelServiceProxy()
        {
			this.Open();
        }

        public UserDisplayModelServiceProxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
			this.Open();
        }

        public UserDisplayModelServiceProxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public UserDisplayModelServiceProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public UserDisplayModelServiceProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
			this.Open();
        }
        #endregion

        #region IUserDisplayModelService
        public int GetCount()
        {
            var result = Channel.GetCount();
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

        public IList<UserDisplayModel> GetAll()
        {
            var result = Channel.GetAll();
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

        public IList<UserDisplayModel> GetPage(int pageIndex, int pageSize)
        {
            var result = Channel.GetPage(pageIndex, pageSize);
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

        public void Delete(UserDisplayModel obj)
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

        public Int64 GetId(UserDisplayModel obj)
        {
            var result = Channel.GetId(obj);
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

        #endregion
    }
}
