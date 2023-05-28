// See https://learn.microsoft.com/fr-fr/dotnet/core/extensions/windows-service
// See https://learn.microsoft.com/fr-fr/dotnet/core/extensions/windows-service-with-installer
// To install this windows service: 
// WindowsService /Install
// <=> sc create ".NET Joke Service" binPath="path/to/App.WindowsService.exe" start=auto
// To uninstall  
// WindowsService /Uninstall
// <=>  sc stop ".NET Joke Service" + sc delete ".NET Joke Service"

using CliWrap;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using NServiceBus;
using NServiceBus.Logging;
using WindowsService;

const string ServiceName = ".NET Joke Service";

if (args is { Length: 1 })
{
  string executablePath =
      Path.Combine(AppContext.BaseDirectory, "WindowsService.exe");

  if (args[0] is "/Install")
  {
    await Cli.Wrap("sc")
        .WithArguments(new[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" })
        .ExecuteAsync();
  }
  else if (args[0] is "/Start")
  {
    await Cli.Wrap("sc")
        .WithArguments(new[] { "start", ServiceName })
        .ExecuteAsync();
  }
  else if (args[0] is "/Stop")
  {
    await Cli.Wrap("sc")
        .WithArguments(new[] { "stop", ServiceName })
        .ExecuteAsync();
  }
  else if (args[0] is "/Delete")
  {
    await Cli.Wrap("sc")
        .WithArguments(new[] { "delete", ServiceName })
        .ExecuteAsync();
  }
  else if (args[0] is "/Uninstall")
  {
    try
    {
      await Cli.Wrap("sc")
          .WithArguments(new[] { "stop", ServiceName })
          .ExecuteAsync();
    }
    finally
    {
      await Cli.Wrap("sc")
          .WithArguments(new[] { "delete", ServiceName })
          .ExecuteAsync();
    }
  }

  return;
}

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
  options.ServiceName = ".NET Joke Service";
});

LoggerProviderOptions
  .RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

builder.Services.AddSingleton<JokeService>();
builder.Services.AddHostedService<WindowsBackgroundService>();

// See: https://github.com/dotnet/runtime/issues/47303
builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));

const string appName = "WindowsService";

var endpointConfiguration = new EndpointConfiguration(appName);
#if DEBUG
var transport = endpointConfiguration.UseTransport<LearningTransport>();
#else
ILoggerProvider loggerprovider = builder.Services.BuildServiceProvider().GetRequiredService<ILoggerProvider>();
LogManager.UseFactory(new MicrosoftNServiceBusLoggerFactory(NServiceBus.Logging.LogLevel.Debug, loggerprovider));

endpointConfiguration.EnableInstallers();
var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
transport.UseConventionalRoutingTopology();
transport.ConnectionString(@"host=localhost:5672;virtualhost=vhost-bhws;username=user-bhws;password=p@ssw0rd;");
#endif

var endpointInstance = await Endpoint.Start(endpointConfiguration)
    .ConfigureAwait(false);

IHost host = builder.Build();
host.Run();

await endpointInstance.Stop()
    .ConfigureAwait(false);