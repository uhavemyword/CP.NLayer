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

    public partial class DepartmentDisplayModelServiceProxy : ClientBase<IDepartmentDisplayModelService>, IDepartmentDisplayModelService
    {
        #region ctor
        public DepartmentDisplayModelServiceProxy()
        {
			this.Open();
        }

        public DepartmentDisplayModelServiceProxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
			this.Open();
        }

        public DepartmentDisplayModelServiceProxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public DepartmentDisplayModelServiceProxy(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
			this.Open();
        }

        public DepartmentDisplayModelServiceProxy(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
			this.Open();
        }
        #endregion

        #region IDepartmentDisplayModelService
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

        public IList<DepartmentDisplayModel> GetAll()
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

        public IList<DepartmentDisplayModel> GetPage(int pageIndex, int pageSize)
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

        public void Delete(DepartmentDisplayModel obj)
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

        public Int64 GetId(DepartmentDisplayModel obj)
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
