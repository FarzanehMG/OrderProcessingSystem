using Contracts;
using InventoryService.Application.Features.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryService.Infrastructure.Consumers;

public sealed class OrderCreatedConsumer(
    ISender sender,
    ILogger<OrderCreatedConsumer> logger)
    : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var msg = context.Message;

        logger.LogInformation(
            "Processing OrderCreatedEvent. OrderId: {OrderId}, ProductId: {ProductId}, Quantity: {Quantity}",
            msg.OrderId, msg.ProductId, msg.Quantity);

        var result = await sender.Send(
            new ReserveInventoryCommand(msg.OrderId, msg.ProductId, msg.Quantity),
            context.CancellationToken);

        if (result.IsSuccess)
        {
            logger.LogInformation("Inventory reserved for order {OrderId}.", msg.OrderId);
            await context.Publish(new InventoryReservedEvent(msg.OrderId));
        }
        else
        {
            logger.LogWarning(
                "Inventory reservation failed for order {OrderId}: {Error}",
                msg.OrderId, result.Error.Message);
            await context.Publish(new InventoryFailedEvent(msg.OrderId, result.Error.Message));
        }
    }
}