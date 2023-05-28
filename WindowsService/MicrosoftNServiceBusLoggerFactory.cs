using NServiceBus.Logging;

namespace WindowsService;

class MicrosoftNServiceBusLoggerFactory : NServiceBus.Logging.ILoggerFactory
{
  private NServiceBus.Logging.LogLevel _level;
  private ILoggerProvider _loggerProvider;

  public MicrosoftNServiceBusLoggerFactory(NServiceBus.Logging.LogLevel level, ILoggerProvider loggerProvider)
  {
    _level = level;
    _loggerProvider = loggerProvider;
  }

  public ILog GetLogger(Type type)
  {
    return GetLogger(type.FullName ?? string.Empty);
  }

  public ILog GetLogger(string name)
  {
    return new MicrosoftNServiceBusLog(name, _level, _loggerProvider);
  }
}