using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Repositories;

namespace OrderService.Infrastructure.Consumers;

public sealed class InventoryFailedConsumer(
    IOrderRepository repository,
    ILogger<InventoryFailedConsumer> logger)
    : IConsumer<InventoryFailedEvent>
{
    public async Task Consume(ConsumeContext<InventoryFailedEvent> context)
    {
        var message = context.Message;

        var order = await repository.GetByIdAsync(message.OrderId, context.CancellationToken);
        if (order is null)
        {
            logger.LogWarning("Order {OrderId} was not found while failing.", message.OrderId);
            return;
        }

        var result = order.Fail("system");
        if (result.IsFailure)
        {
            logger.LogWarning("Order {OrderId} skipped: {Error}", message.OrderId, result.Error.Message);
            return;
        }

        await repository.SaveChangesAsync(context.CancellationToken);
        logger.LogInformation("Order {OrderId} failed. Reason: {Reason}", message.OrderId, message.Reason);
    }
}