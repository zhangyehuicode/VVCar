using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Wcf
{
    public class LoggingOperationInvoker : IOperationInvoker
    {
        private readonly IOperationInvoker _OriginalInvoker;
        private System.Diagnostics.Stopwatch _Watch;

        public LoggingOperationInvoker(IOperationInvoker invoker)
        {
            _OriginalInvoker = invoker;
        }

        #region IOperationInvoker 成员

        public object[] AllocateInputs()
        {
            return _OriginalInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            object result = null; outputs = null;
            BeforeInvoke();
            try
            {
                result = _OriginalInvoker.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                YEF.Core.Logging.LoggerManager.GetLogger("System").Error("call {0} had exception:{1}", GetActionName(), ex);
                throw ex;
            }
            AfterInvoke();
            return result;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return _OriginalInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            var invokeResult = _OriginalInvoker.InvokeEnd(instance, out outputs, result);
            AfterInvoke();
            return invokeResult;
        }

        public bool IsSynchronous
        {
            get { return _OriginalInvoker.IsSynchronous; }
        }

        #endregion

        void BeforeInvoke()
        {
            this._Watch = System.Diagnostics.Stopwatch.StartNew();
        }

        void AfterInvoke()
        {
            this._Watch.Stop();
            var usedTime = this._Watch.ElapsedMilliseconds;
            if (usedTime > 1500)
            {
                YEF.Core.Logging.LoggerManager.GetLogger("System").Warn("run {0} cost time = {1} (ms) !!!", GetActionName(), usedTime);
            }
            else
            {
                YEF.Core.Logging.LoggerManager.GetLogger("System").Debug("run {0} cost time = {1} (ms).", GetActionName(), usedTime);
            }
        }

        string GetActionName()
        {
            var action = OperationContext.Current.IncomingMessageHeaders.Action;
            if (string.IsNullOrEmpty(action))
                return action;
            action = action.Replace("http://tempuri.org/", "").Replace('/', '.');
            return action;
        }
    }
}
