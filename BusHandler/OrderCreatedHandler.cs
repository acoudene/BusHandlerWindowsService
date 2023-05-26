using Messages;
using NServiceBus;
using NServiceBus.Logging;
using WrapperBusinessLibrary;

namespace BusHandler;

public class OrderCreatedHandler : IHandleMessages<OrderCreated>
{
  static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

  public Task Handle(OrderCreated message, IMessageHandlerContext context)
  {
    log.Info($"Received OrderCreated, OrderId = {message.OrderId}");

    var wrapper = new WrapperBusinessClass();
    string description = wrapper.GetDescription();

    log.Info($"Do stuff for OrderId = {message.OrderId} on wrapper and cpp libraries, found description = {description}");

    return Task.CompletedTask;
  }
}
