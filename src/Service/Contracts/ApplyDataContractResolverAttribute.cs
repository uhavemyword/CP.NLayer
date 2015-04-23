// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/21/2013 11:26:32 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Contracts
{
    using System;
    using System.Data.Entity.Core.Objects;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// The Attribute will tell DataContractSerializer to use the ProxyDataContractResolver class in service operations, to serialize POCO proxies as POCO entities.
    /// The POCO proxy type cannot be directly serialized or deserialized by the Windows Communication Foundation (WCF),
    /// because the DataContractSerializer serialization engine can only serialize and deserialize known types. The proxy type is not a known type.
    /// See http://technet.microsoft.com/en-us/library/ee705457.aspx for more information.
    /// </summary>
    public class ApplyDataContractResolverAttribute : Attribute, IOperationBehavior
    {
        public ApplyDataContractResolverAttribute()
        {
        }

        public void AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
        {
            DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            dataContractSerializerOperationBehavior.DataContractResolver = new ProxyDataContractResolver();
        }

        public void ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
        {
            DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            dataContractSerializerOperationBehavior.DataContractResolver = new ProxyDataContractResolver();
        }

        public void Validate(OperationDescription description)
        {
            // Do validation.
        }
    }
}