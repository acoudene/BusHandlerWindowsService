// See https://aka.ms/new-console-template for more information
using Messages;
using NServiceBus;
using System;

const string appName = "SubscriberApp";
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

var scanner = endpointConfiguration.AssemblyScanner();
scanner.ExcludeTypes(typeof(OrderCreated));

var endpointInstance = await Endpoint.Start(endpointConfiguration)
    .ConfigureAwait(false);

Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

await endpointInstance.Stop()
    .ConfigureAwait(false);