using Contracts;
using MediatR;
using OrderService.Application.Abstractions;
using OrderService.Application.Features.Commands;
using OrderService.Application.Repositories;
using OrderService.Domain.Common;
using OrderService.Domain.Entities;

namespace OrderService.Application.Features.Handlers;

public sealed class CreateOrderCommandHandler(
    IOrderRepository repository,
    IMessagePublisher publisher)
    : IRequestHandler<CreateOrderCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var orderResult = OrderEntity.Create(
            request.ProductId,
            request.Quantity,
            "system");

        if (orderResult.IsFailure)
            return orderResult.Error;

        var order = orderResult.Value;

        await repository.AddAsync(order, ct);
        await repository.SaveChangesAsync(ct);

        await publisher.PublishAsync(
            new OrderCreatedEvent(
                order.Id,
                order.ProductId,
                order.Quantity),
                ct);

        await repository.SaveChangesAsync(ct);

        return order.Id;
    }
}