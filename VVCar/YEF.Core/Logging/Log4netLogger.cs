using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Logging
{
    /// <summary>
    /// log4net日志记录器
    /// </summary>
    public class Log4netLogger : ILogger
    {
        private readonly log4net.ILog _logger;

        #region ctor.

        public Log4netLogger(string name)
        {
            _logger = log4net.LogManager.GetLogger(name);
        }

        static Log4netLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        #endregion

        #region ILog 成员

        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        public void Debug(string format, params object[] args)
        {
            _logger.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            _logger.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        public void Warn(string format, params object[] args)
        {
            _logger.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Error(string format, params object[] args)
        {
            _logger.ErrorFormat(format, args);
        }

        public void Error(string format, Exception exception, params object[] args)
        {
            var message = args.Length > 0 ? string.Format(format, args) : format;
            _logger.Error(message, exception);
        }

        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string format, params object[] args)
        {
            _logger.FatalFormat(format, args);
        }

        public void Fatal(string format, Exception exception, params object[] args)
        {
            var message = args.Length > 0 ? string.Format(format, args) : format;
            _logger.Fatal(message, exception);
        }

        #endregion
    }
}
