using System;
using log4net;

namespace Infrastructure.Logger
{
    public sealed class Log4NetLoggerFactory : ILoggerFactory
    {
        static Log4NetLoggerFactory()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}