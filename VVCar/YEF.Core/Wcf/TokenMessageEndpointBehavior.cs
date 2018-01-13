using System;
using System.ServiceModel.Description;

namespace YEF.Core.Wcf
{
    public class TokenMessageEndpointBehavior : IEndpointBehavior
    {
        #region IEndpointBehavior 成员

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            if (clientRuntime != null)
            {
                clientRuntime.MessageInspectors.Add(new ClientTokenMessageInspector());
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            if (endpointDispatcher != null)
            {
                endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new DispatchTokenMessageInspector());
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
