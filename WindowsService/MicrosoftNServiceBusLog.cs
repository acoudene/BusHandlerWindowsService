using NServiceBus.Logging;

namespace WindowsService;

public class MicrosoftNServiceBusLog : ILog
{
  private string _name;
  public bool IsDebugEnabled { get; }

  public bool IsInfoEnabled { get; }

  public bool IsWarnEnabled { get; }

  public bool IsErrorEnabled { get; }

  public bool IsFatalEnabled { get; }

  private ILoggerProvider _loggerProvider;

  public MicrosoftNServiceBusLog(string name, NServiceBus.Logging.LogLevel level, ILoggerProvider loggerProvider)
  {
    _name = name;
    IsDebugEnabled = NServiceBus.Logging.LogLevel.Debug >= level;
    IsInfoEnabled = NServiceBus.Logging.LogLevel.Info >= level;
    IsWarnEnabled = NServiceBus.Logging.LogLevel.Warn >= level;
    IsErrorEnabled = NServiceBus.Logging.LogLevel.Error >= level;
    IsFatalEnabled = NServiceBus.Logging.LogLevel.Fatal >= level;
    _loggerProvider = loggerProvider;
  }
  void Write(Microsoft.Extensions.Logging.LogLevel level, string message, Exception exception)
  {
    var logger = _loggerProvider.CreateLogger(_name);
    logger.Log(level, message, exception);
  }
  void Write(Microsoft.Extensions.Logging.LogLevel level, string message)
  {
    var logger = _loggerProvider.CreateLogger(_name);
    logger.Log(level, message);
  }

  void Write(Microsoft.Extensions.Logging.LogLevel level, string format, params object[] args)
  {
    var logger = _loggerProvider.CreateLogger(_name);
    logger.Log(level, format, args);
  }

  public void Debug(string message)
  {
    if (IsDebugEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Debug, message);
    }
  }

  public void Debug(string message, Exception exception)
  {
    if (IsDebugEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Debug, message, exception);
    }
  }

  public void DebugFormat(string format, params object[] args)
  {
    if (IsDebugEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Debug, format, args);
    }
  }

  public void Error(string message)
  {
    if (IsErrorEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Error, message);
    }
  }

  public void Error(string message, Exception exception)
  {
    if (IsErrorEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Error, message, exception);
    }
  }

  public void ErrorFormat(string format, params object[] args)
  {
    if (IsErrorEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Error, format, args);
    }
  }



  public void Fatal(string message)
  {
    if (IsFatalEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Critical, message);
    }
  }

  public void Fatal(string message, Exception exception)
  {
    if (IsFatalEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Critical, message, exception);
    }
  }

  public void FatalFormat(string format, params object[] args)
  {
    if (IsFatalEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Critical, format, args);
    }
  }

  public void Info(string message)
  {
    if (IsInfoEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Information, message);
    }
  }

  public void Info(string message, Exception exception)
  {
    if (IsInfoEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Information, message, exception);
    }
  }

  public void InfoFormat(string format, params object[] args)
  {
    if (IsInfoEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Information, format, args);
    }
  }

  public void Warn(string message)
  {
    if (IsWarnEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Warning, message);
    }
  }

  public void Warn(string message, Exception exception)
  {
    if (IsWarnEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Warning, message, exception);
    }
  }

  public void WarnFormat(string format, params object[] args)
  {
    if (IsWarnEnabled)
    {
      Write(Microsoft.Extensions.Logging.LogLevel.Warning, format, args);
    }
  }
}