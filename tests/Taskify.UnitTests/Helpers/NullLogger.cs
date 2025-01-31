namespace Taskify.UnitTests.Helpers;

using Microsoft.Extensions.Logging;

using System;

public class NullLogger : NullLogger<NullLogger>
{ 
}

public class NullLogger<T> : ILogger<T>
{
    public T? Caller { get; set; }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
    }
}