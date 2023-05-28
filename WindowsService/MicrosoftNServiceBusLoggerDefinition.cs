using NServiceBus.Logging;

namespace WindowsService;

public class MicrosoftNServiceBusLoggerDefinition : LoggingFactoryDefinition
{
  private NServiceBus.Logging.LogLevel _level = NServiceBus.Logging.LogLevel.Info;
  private ILoggerProvider _loggerProvider;

  public void Level(NServiceBus.Logging.LogLevel level, ILoggerProvider loggerProvider)
  {
    _level = level;
    _loggerProvider = loggerProvider;
  }

  protected override NServiceBus.Logging.ILoggerFactory GetLoggingFactory()
  {
    return new MicrosoftNServiceBusLoggerFactory(_level, _loggerProvider);
  }
}
