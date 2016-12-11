using System;
using log4net;

namespace Infrastructure.Logger
{
    public interface ILoggerFactory
    {
        ILog GetLogger(Type type);
    }
}