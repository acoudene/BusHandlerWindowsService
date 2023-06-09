﻿using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace BusHandler;

public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
{
  static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

  public Task Handle(PlaceOrder message, IMessageHandlerContext context)
  {
    log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");
    var orderCreated = new OrderCreated() { OrderId = message.OrderId };
    return context.Publish(orderCreated);
  }
}
