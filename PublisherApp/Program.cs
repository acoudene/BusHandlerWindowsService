// See https://aka.ms/new-console-template for more information

using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace PublisherApp;

internal class Program
{
  static ILog log = LogManager.GetLogger<Program>();

  static async Task RunLoop(IEndpointInstance endpointInstance)
  {
    while (true)
    {
      log.Info("Press 'P' to place an order, or 'Q' to quit.");
      var key = Console.ReadKey();
      Console.WriteLine();

      switch (key.Key)
      {
        case ConsoleKey.P:
          // Instantiate the command
          var command = new PlaceOrder
          {
            OrderId = Guid.NewGuid().ToString()
          };

          // Send the command to the local endpoint
          log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
          await endpointInstance.Send(command)
              .ConfigureAwait(false);

          break;

        case ConsoleKey.Q:
          return;

        default:
          log.Info("Unknown input. Please try again.");
          break;
      }
    }
  }

  static async Task Main(string[] args)
  {    
    const string appName = "PublisherApp";
    Console.Title = appName;

    var endpointConfiguration = new EndpointConfiguration(appName);

#if DEBUG
    var transport = endpointConfiguration.UseTransport<LearningTransport>();
#else
    endpointConfiguration.EnableInstallers();
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.UseConventionalRoutingTopology();
    transport.ConnectionString(@"host=localhost:5672;virtualhost=vhost-bhws;username=user-bhws;password=p@ssw0rd;");
#endif

    transport.Routing().RouteToEndpoint(typeof(PlaceOrder), "SubscriberApp");

    var endpointInstance = await Endpoint.Start(endpointConfiguration)
        .ConfigureAwait(false);

    // Replace with:
    await RunLoop(endpointInstance)
        .ConfigureAwait(false);

    await endpointInstance.Stop()
        .ConfigureAwait(false);
  }
}
