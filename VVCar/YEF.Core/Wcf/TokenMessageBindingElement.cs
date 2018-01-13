using System;
using System.ServiceModel.Configuration;

namespace YEF.Core.Wcf
{
    internal class TokenMessageBindingElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(TokenMessageEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new TokenMessageEndpointBehavior();
        }
    }
}
