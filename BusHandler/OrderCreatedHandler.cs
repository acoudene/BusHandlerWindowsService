using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace BusHandler;

public class OrderCreatedHandler : IHandleMessages<OrderCreated>
{
  static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

  public Task Handle(OrderCreated message, IMessageHandlerContext context)
  {
    log.Info($"Received OrderCreated, OrderId = {message.OrderId}");
    return Task.CompletedTask;
  }
}
