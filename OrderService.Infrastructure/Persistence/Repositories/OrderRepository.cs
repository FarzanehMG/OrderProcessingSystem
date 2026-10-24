using Microsoft.EntityFrameworkCore;
using OrderService.Application.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Persistence.Repositories;

internal sealed class OrderRepository(OrderDbContext db) : IOrderRepository
{
    public async Task AddAsync(OrderEntity order, CancellationToken ct = default)
        => await db.Orders.AddAsync(order, ct);

    public Task<OrderEntity?> GetByIdAsync(long orderId, CancellationToken ct = default)
        => db.Orders.FirstOrDefaultAsync(x => x.Id == orderId, ct);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => db.SaveChangesAsync(ct);
}