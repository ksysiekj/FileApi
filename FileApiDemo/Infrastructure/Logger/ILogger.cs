using System;

namespace Infrastructure.Logger
{
    public interface ILogger<T>
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message, Exception exception = null);
        void Error(string message, Exception exception = null);
    }
}