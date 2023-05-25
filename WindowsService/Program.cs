// See https://learn.microsoft.com/fr-fr/dotnet/core/extensions/windows-service
// See https://learn.microsoft.com/fr-fr/dotnet/core/extensions/windows-service-with-installer
// To install this windows service: 
// WindowsService /Install
// <=> sc create ".NET Joke Service" binPath="path/to/App.WindowsService.exe" start=auto
// To uninstall  
// WindowsService /Uninstall
// <=>  sc stop ".NET Joke Service" + sc delete ".NET Joke Service"

using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using WindowsService;
using CliWrap;

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
  else if (args[0] is "/Uninstall")
  {
    await Cli.Wrap("sc")
        .WithArguments(new[] { "stop", ServiceName })
        .ExecuteAsync();

    await Cli.Wrap("sc")
        .WithArguments(new[] { "delete", ServiceName })
        .ExecuteAsync();
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

IHost host = builder.Build();
host.Run();