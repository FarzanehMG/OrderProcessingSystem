using MediatR;
using OrderService.Application.Features.Dtos;
using OrderService.Application.Features.Queries;
using OrderService.Application.Repositories;
using OrderService.Domain.Common;
using OrderService.Domain.Errors;

namespace OrderService.Application.Features.Handlers;

public sealed class GetOrderByIdQueryHandler(IOrderRepository repository)
    : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken ct)
    {
        var order = await repository.GetByIdAsync(request.Id, ct);
        if (order is null)
            return OrderErrors.NotFound(request.Id);

        return new OrderDto(order.Id, order.ProductId, order.Quantity,
            order.OrderStatus.ToString(), order.CreatedDate);
    }
}