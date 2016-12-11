using log4net;
using System;

namespace Infrastructure.Logger
{
    public class Logger<T> : ILogger<T>
    {
        private readonly ILoggerFactory _loggerFactory;
        private ILog _log;

        private ILog Log => _log ?? (_log = _loggerFactory.GetLogger(typeof(T)));


        public Logger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }


        // 225 000

        public void Debug(string message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }

        public void Info(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public void Warning(string message, Exception exception = null)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message, exception);
            }
        }

        public void Error(string message, Exception exception = null)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(message, exception);
            }
        }
    }
}
