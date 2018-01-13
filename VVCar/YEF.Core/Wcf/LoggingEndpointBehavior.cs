using System;
using System.ServiceModel.Description;

namespace YEF.Core.Wcf
{
    public class LoggingEndpointBehavior : IEndpointBehavior
    {
        #region IEndpointBehavior 成员

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            if (endpoint != null)
            {
                foreach (var operation in endpoint.Contract.Operations)
                {
                    operation.Behaviors.Add(new LoggingOperationBehavior());
                }
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            if (endpoint != null)
            {
                foreach (var operation in endpoint.Contract.Operations)
                {
                    operation.Behaviors.Add(new LoggingOperationBehavior());
                }
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
