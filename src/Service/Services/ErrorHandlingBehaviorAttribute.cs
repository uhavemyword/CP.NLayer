// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 3/1/2013 2:57:46 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Common;
    using System;
    using System.Collections.ObjectModel;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class ErrorHandlingBehaviorAttribute : Attribute, IServiceBehavior, IErrorHandler
    {
        // Implement IErrorHandler:
        // Implement the ProvideFault method to control the fault message that is returned to the client.
        // Implement the HandleError method to ensure error-related behaviors, including error logging, assuring a fail fast, shutting down the application, and so on.
        // http://msdn.microsoft.com/en-us/library/system.servicemodel.dispatcher.ierrorhandler.aspx
        // http://www.neovolve.com/post/2008/04/07/implementing-ierrorhandler.aspx

        #region IErrorHandler

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var message = error.CustomMessage();
            message = string.IsNullOrEmpty(message) ? error.Message : message;
            var faultException = new FaultException(message);
            faultException.Source = error.Source;
            MessageFault essageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, essageFault, faultException.Action);
        }

        public bool HandleError(Exception error)
        {
            var message = error.CustomMessage();
            if (string.IsNullOrEmpty(message))
            {
                log4net.LogManager.GetLogger(typeof(ErrorHandlingBehaviorAttribute)).Error("Unexcepted service exception occured.", error);
            }

            return false; // False to abort the session.
        }

        #endregion

        #region IServiceBehavior Members

        // This behavior modifies no binding parameters.
        public void AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
        {
            return;
        }

        // This behavior is an IErrorHandler implementation and
        // must be applied to each ChannelDispatcher.
        public void ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
            {
                chanDisp.ErrorHandlers.Add(this);
            }
            log4net.LogManager.GetLogger(description.ServiceType).Info("The ErrorHandlingBehaviorAttribute has been applied.");
        }

        public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
        {
            return;
        }

        #endregion
    }
}