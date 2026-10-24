using OrderService.Domain.Entities;

namespace OrderService.Application.Repositories;

public interface IOrderRepository
{
    Task AddAsync(OrderEntity order, CancellationToken ct = default);
    Task<OrderEntity?> GetByIdAsync(long orderId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}