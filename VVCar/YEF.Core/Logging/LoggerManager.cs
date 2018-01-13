using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace YEF.Core.Logging
{
    /// <summary>
    /// Logger管理器
    /// </summary>
    public static class LoggerManager
    {
        private static readonly ConcurrentDictionary<string, ILogger> Loggers;
        private static readonly object LockObj = new object();

        static LoggerManager()
        {
            Loggers = new ConcurrentDictionary<string, ILogger>();
        }

        public static ILogger GetLogger()
        {
            return GetLogger("Application");
        }

        public static ILogger GetLogger(string name)
        {
            ILogger logger;
            if (Loggers.TryGetValue(name, out logger))
            {
                return logger;
            }
            logger = new Log4netLogger(name);
            Loggers[name] = logger;
            return logger;
        }
    }
}
