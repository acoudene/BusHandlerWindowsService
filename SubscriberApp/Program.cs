// See https://aka.ms/new-console-template for more information
using Messages;
using NServiceBus;
using System;

const string appName = "SubscriberApp";
Console.Title = appName;

var endpointConfiguration = new EndpointConfiguration(appName);
var transport = endpointConfiguration.UseTransport<LearningTransport>();
var scanner = endpointConfiguration.AssemblyScanner();
scanner.ExcludeTypes(typeof(OrderCreated));

var endpointInstance = await Endpoint.Start(endpointConfiguration)
    .ConfigureAwait(false);

Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

await endpointInstance.Stop()
    .ConfigureAwait(false);