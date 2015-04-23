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

    public partial class ConfigServiceProxy : ClientBase<IConfigService>, IConfigService
    {
        #region ctor
        public ConfigServiceProxy()
        {
			this.Open();
        }

        public ConfigServiceProxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
			this.Open();
        }

        public ConfigServiceProxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public ConfigServiceProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public ConfigServiceProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
			this.Open();
        }
        #endregion

        #region IConfigService
        public Config GetByCategoryAndName(string category, string name)
        {
            var result = Channel.GetByCategoryAndName(category, name);
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

        public string GetValue(string category, string name)
        {
            var result = Channel.GetValue(category, name);
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

        public void SetValue(string category, string name, string value)
        {
            Channel.SetValue(category, name, value);
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

        public IList<Config> GetAll()
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

        public Config GetById(Int64 id)
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

        public IList<Config> GetPage(int pageIndex, int pageSize)
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

        public Config Insert(Config obj)
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

        public void Update(Config obj)
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

        public void Delete(Config obj)
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
