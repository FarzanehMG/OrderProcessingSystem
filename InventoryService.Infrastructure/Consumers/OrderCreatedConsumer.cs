using Contracts;
using InventoryService.Application.Features.Commands;
using MassTransit;
using MediatR;

namespace InventoryService.Infrastructure.Consumers;

public sealed class OrderCreatedConsumer(ISender sender) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var msg = context.Message;

        var result = await sender.Send(
            new ReserveInventoryCommand(msg.OrderId, msg.ProductId, msg.Quantity),
            context.CancellationToken);

        if (result.IsSuccess)
            await context.Publish(new InventoryReservedEvent(msg.OrderId));
        else
            await context.Publish(new InventoryFailedEvent(msg.OrderId, result.Error.Message));
    }
}