using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Repositories;

namespace OrderService.Infrastructure.Consumers;

public sealed class InventoryReservedConsumer(
    IOrderRepository repository,
    ILogger<InventoryReservedConsumer> logger)
    : IConsumer<InventoryReservedEvent>
{
    public async Task Consume(ConsumeContext<InventoryReservedEvent> context)
    {
        var orderId = context.Message.OrderId;

        var order = await repository.GetByIdAsync(orderId, context.CancellationToken);
        if (order is null)
        {
            logger.LogWarning("Order {OrderId} was not found while confirming.", orderId);
            return;
        }

        var result = order.Confirm("system");
        if (result.IsFailure)
        {
            // پیام تکراری یا سفارش قبلاً نهایی شده؛ خطا نمی‌دهیم تا retry بی‌خود نشود
            logger.LogWarning("Order {OrderId} skipped: {Error}", orderId, result.Error.Message);
            return;
        }

        await repository.SaveChangesAsync(context.CancellationToken);
        logger.LogInformation("Order {OrderId} confirmed.", orderId);
    }
}