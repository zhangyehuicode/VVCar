using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace YEF.Core.Wcf
{
    public class DispatchTokenMessageInspector : IDispatchMessageInspector
    {

        #region IDispatchMessageInspector 成员

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            if (OperationContext.Current.IncomingMessageHeaders.Action.Contains("Login"))
            {
                return null;
            }
            string token = GetHeaderValue("token");
            if (string.IsNullOrEmpty(token))
            {
                //AppContext.CurrentSession.UserId = "0000001";
                //AppContext.CurrentSession.UserName = "超级用户";
                //AppContext.CurrentSession.DepartmentId = "DEPT01";
                //AppContext.CurrentSession.DepartmentName = "部门01";
                //throw new YEF.Core.Exceptions.NoSessionException("没有登录或长时间没有进行任何操作，请重新登录。");
            }
            if (token != null)
            {

            }
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }

        #endregion

        string GetHeaderValue(string name)
        {
            string result = null;
            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader(name, "http://tempuri.org");
            if (index >= 0)
            {
                result = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
            }
            return result;
        }
    }

    public class ClientTokenMessageInspector : IClientMessageInspector
    {
        #region IClientMessageInspector 成员

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var tokenHeader = CreateMessageHeader("token", "token_value");
            request.Headers.Add(tokenHeader);
            return null;
        }

        #endregion

        MessageHeader CreateMessageHeader(string name, string value)
        {
            return MessageHeader.CreateHeader(name, "http://tempuri.org", value, false, string.Empty);
        }
    }
}
